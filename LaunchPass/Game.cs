﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace RetroPass
{
    public abstract class Game
    {
        [XmlIgnore] public virtual string Title { get; set; }
        [XmlIgnore] public string DataRootFolder;
        [XmlIgnore] public virtual Platform GamePlatform { get; set; }
        [XmlIgnore] public virtual string CoreName { get; set; }
        [XmlIgnore] public virtual string ApplicationPath { get; set; }
        [XmlIgnore] public virtual string ApplicationPathFull { get; set; }
        [XmlIgnore] public virtual string BoxFrontFileName { get; set; }
        [XmlIgnore] public virtual string BoxFrontContentName { get; set; }
        [XmlIgnore] public virtual string BoxFrontFilePath { get; set; } //cache path to box
        [XmlIgnore] public string VideoTitle { get; set; }
        [XmlIgnore] public StorageFile ImageFile { get; set; }
        [XmlIgnore] public virtual string Description { get; set; }
        [XmlIgnore] public virtual string Developer { get; set; }
        [XmlIgnore] public virtual string Publisher { get; set; }
        [XmlIgnore] public virtual string ReleaseDate { get; set; }
        [XmlIgnore] public virtual string Genre { get; set; }
        [XmlIgnore] public virtual string PlayMode { get; set; }
        [XmlIgnore] public virtual string ReleaseType { get; set; }
        [XmlIgnore] public virtual string Version { get; set; }
        [XmlIgnore] public virtual string MaxPlayers { get; set; }
        [XmlIgnore] public virtual string PlayTime { get; set; }

        BitmapImage bitmapImage = null;

        public abstract void Init();

        private async Task<StorageFile> FindImageThumbnailAsync(StorageFolder folder)
        {
            StorageFile boxFrontFile = null;

            Trace.TraceInformation($"Game: FindImageThumbnailAsync: {BoxFrontFileName}");

            if (string.IsNullOrEmpty(BoxFrontFilePath))
            {
                var options = new QueryOptions
                {
                    ApplicationSearchFilter =
                        $"System.FileName:~\"{BoxFrontFileName}.???\"" +
                        $" OR System.FileName:~\"{BoxFrontFileName}-*.???\"" +
                        $" OR System.FileName:~\"{BoxFrontContentName}.???\"" +
                        $" OR System.FileName:~\"{BoxFrontContentName}-*.???\"",
                    FolderDepth = FolderDepth.Deep
                };
                var queryResult = folder.CreateFileQueryWithOptions(options);

                var sortedFiles = await queryResult.GetFilesAsync();

                var preferredRegions = new[] { "North America", "Europe", "United States", "Australia", "Canada", "Box - Front" };
                var currentRegionIndex = 100;
                var preferredFileIndex = -1;

                Trace.TraceInformation($"Game: Found {sortedFiles.Count} files for {BoxFrontFileName}");

                preferredFileIndex = sortedFiles.Count > 0 ? 0 : -1;

                for (int i = 0; i < sortedFiles.Count; i++)
                {
                    var item = sortedFiles[i];
                    var parentDirectory = Path.GetFileName(Path.GetDirectoryName(item.Path));

                    var regionIndex = Array.IndexOf(preferredRegions, parentDirectory);
                    if (regionIndex != -1 && regionIndex < currentRegionIndex)
                    {
                        currentRegionIndex = regionIndex;
                        preferredFileIndex = i;

                        currentRegionIndex = currentRegionIndex == 0 ? currentRegionIndex : currentRegionIndex;
                    }
                }

                boxFrontFile = preferredFileIndex != -1 ? sortedFiles[preferredFileIndex] : null;
                BoxFrontFilePath = boxFrontFile != null ? boxFrontFile.Path : null;
            }
            else
            {
                var file = await StorageFile.GetFileFromPathAsync(BoxFrontFilePath);
                boxFrontFile = file;
            }

            if (boxFrontFile == null)
            {
                Trace.TraceWarning($"Game: No Box Front File: {BoxFrontFileName}");
                return null;
            }
            else
            {
                Trace.TraceInformation($"Get Box Front File: {boxFrontFile.Path}");
                return boxFrontFile;
            }
        }

        public async Task<BitmapImage> GetImageThumbnailAsync()
        {
            StorageFile ImageFile = null;
            StorageItemThumbnail thumbnail = null;
            //BitmapImage bitmapImage = null;

            Trace.TraceInformation("Game: GetImageThumbnailAsync {0}", BoxFrontFileName);

            if (bitmapImage != null)
            {
                return bitmapImage;
            }

            if (thumbnail == null && string.IsNullOrEmpty(BoxFrontFileName) == false)
            {
                if (GamePlatform.BoxFrontFolder == null)
                {
                    GamePlatform.BoxFrontFolder = await StorageUtils.GetFolderFromPathAsync(GamePlatform.BoxFrontPath);
                }

                StorageFolder folder = GamePlatform.BoxFrontFolder;

                ImageFile = await FindImageThumbnailAsync(folder);
                //check if thumbs file exists
                if (ImageFile != null)
                {
                    bitmapImage = await ThumbnailCache.Instance.GetThumbnailAsync(ImageFile);
                }
            }

            return bitmapImage;
        }

        public async Task<BitmapImage> GetMainImageAsync()
        {
            BitmapImage bitmapImage = null;

            Trace.TraceInformation("Game: GetMainImageAsync {0}", BoxFrontFilePath);

            if (string.IsNullOrEmpty(BoxFrontFilePath) == false)
            {
                var boxFrontFile = await StorageFile.GetFileFromPathAsync(BoxFrontFilePath);
                using (IRandomAccessStream fileStream = await boxFrontFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap
                    bitmapImage = new BitmapImage();
                    // Decode pixel sizes are optional
                    // It's generally a good optimisation to decode to match the size you'll display
                    //bitmapImage.DecodePixelHeight = decodePixelHeight;
                    //bitmapImage.DecodePixelWidth = decodePixelWidth;

                    await bitmapImage.SetSourceAsync(fileStream);
                    Trace.TraceInformation("Game: GetMainImageAsync SUCCESS {0}", BoxFrontFilePath);
                }
            }

            return bitmapImage;
        }

        public async Task<MediaSource> GetVideo()
        {
            MediaSource mediaSource = null;
            //find file
            if (string.IsNullOrEmpty(GamePlatform.VideoPath))
            {
                return null;
            }

            StorageFolder folder = await StorageUtils.GetFolderFromPathAsync(GamePlatform.VideoPath);

            if (folder == null)
            {
                return null;
            }

            QueryOptions options = new QueryOptions();
            options.ApplicationSearchFilter =
                "System.FileName:~\"" + VideoTitle + ".???" + "\"" + " OR " +
                "System.FileName:~\"" + VideoTitle + "-*.???" + "\"" + " OR " +
                "System.FileName:~\"" + BoxFrontContentName + ".???" + "\"" + " OR " +
                "System.FileName:~\"" + BoxFrontContentName + "-*.???" + "\"";

            options.FolderDepth = FolderDepth.Deep;
            options.SetThumbnailPrefetch(ThumbnailMode.SingleItem, 100, ThumbnailOptions.ReturnOnlyIfCached);
            StorageFileQueryResult queryResult = folder.CreateFileQueryWithOptions(options);
            IReadOnlyList<StorageFile> sortedFiles = await queryResult.GetFilesAsync();

            //by default take the first available
            if (sortedFiles.Count > 0)
            {
                mediaSource = MediaSource.CreateFromStorageFile(sortedFiles[0]);
                Trace.TraceInformation("Game: GetVideo SUCCESS {0}", sortedFiles[0].Path);
            }

            return mediaSource;
        }

        public async Task<ObservableCollection<DetailImage>> GetDetailsImages(ObservableCollection<DetailImage> imageList)
        {
            List<string> gameplayFolders = new List<string>();

            if (string.IsNullOrEmpty(GamePlatform.ScreenshotGameTitlePath) == false)
            {
                gameplayFolders.Add(GamePlatform.ScreenshotGameTitlePath);
            }
            if (string.IsNullOrEmpty(GamePlatform.ScreenshotGameSelectPath) == false)
            {
                gameplayFolders.Add(GamePlatform.ScreenshotGameSelectPath);
            }
            if (string.IsNullOrEmpty(GamePlatform.ScreenshotGameplayPath) == false)
            {
                gameplayFolders.Add(GamePlatform.ScreenshotGameplayPath);
            }

            foreach (var gameplayFolder in gameplayFolders)
            {
                //search for images for this game
                StorageFolder folder = await StorageUtils.GetFolderFromPathAsync(gameplayFolder);
                QueryOptions options = new QueryOptions();
                options.ApplicationSearchFilter =
                    "System.FileName:~\"" + BoxFrontFileName + ".???" + "\"" + " OR " +
                    "System.FileName:~\"" + BoxFrontFileName + "-*.???" + "\"" + " OR " +
                    "System.FileName:~\"" + BoxFrontContentName + ".???" + "\"" + " OR " +
                    "System.FileName:~\"" + BoxFrontContentName + "-*.???" + "\"";
                options.FolderDepth = FolderDepth.Deep;
                StorageFileQueryResult queryResult = folder.CreateFileQueryWithOptions(options);

                IReadOnlyList<StorageFile> sortedFiles = await queryResult.GetFilesAsync();
                int count = sortedFiles.Count();
                if (gameplayFolder == GamePlatform.ScreenshotGameTitlePath || gameplayFolder == GamePlatform.ScreenshotGameSelectPath)
                {
                    count = 1;
                }

                foreach (StorageFile item in sortedFiles.Take(count))
                {
                    StorageFile ImageFile = await StorageFile.GetFileFromPathAsync(item.Path);

                    using (IRandomAccessStream fileStream = await ImageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        // Set the image source to the selected bitmap
                        BitmapImage bitmapImage = new BitmapImage();
                        // Decode pixel sizes are optional
                        // It's generally a good optimization to decode to match the size you'll display
                        //bitmapImage.DecodePixelHeight = decodePixelHeight;//478
                        //bitmapImage.DecodePixelWidth = decodePixelWidth;

                        await bitmapImage.SetSourceAsync(fileStream);
                        DetailImage dt = new DetailImage();
                        dt.image = bitmapImage;
                        dt.path = item.Path;
                        imageList.Add(dt);

                        Trace.TraceInformation("Game: GetDetailsImages SUCCESS {0}", dt.path);
                    }
                }
            }

            return imageList;
        }
    }
}
