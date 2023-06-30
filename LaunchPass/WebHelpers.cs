using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http.Headers;
using Windows.Web.Http;
using Windows.UI;
using System.IO;
using Windows.ApplicationModel;
using Windows.UI.Popups;
using Windows.ApplicationModel.Core;

namespace LaunchPass
{
    public static class WebHelpers
    {
        public static string updateFile = "https://basharast.github.io/wut/app/WUTB.txt";
        public static string updatePage = "https://basharast.github.io";
        public static async void CheckForUpdates()
        {
            var updateState = await CheckForNewUpdates();
            if (updateState.newUpdate)
            {
                var messageDialog = new MessageDialog($"New update avialable ({updateState.updateVersion})\nDo you want to download it now?");
                messageDialog.Commands.Add(new UICommand("Download", new UICommandInvokedHandler(CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Later"));
                await messageDialog.ShowAsync();
            }
        }
        private static async void CommandInvokedHandler(IUICommand command)
        {
            var updateURI = new Uri(updatePage);
            var options = new Windows.System.LauncherOptions();
            options.PreferredApplicationPackageFamilyName = "Microsoft.MicrosoftEdge_8wekyb3d8bbwe";
            options.PreferredApplicationDisplayName = "Microsoft Edge";
            // Launch the URI
            //Try with Edge specifically
            var success = await Windows.System.Launcher.LaunchUriAsync(updateURI, options);
            if (!success)
            {
                success = await Windows.System.Launcher.LaunchUriAsync(updateURI);
            }
        }

        public static async Task<UpdateState> CheckForNewUpdates()
        {
            var updateState = new UpdateState(false, "1.0.0");
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var timeStamp = new TimeSpan(DateTime.Now.Ticks);
            var time = timeStamp.Milliseconds;
            var unCahcedLink = $"{updateFile}?time={time}";
            var testResponse = await GetResponse(unCahcedLink, cancellationTokenSource.Token);
            if (testResponse != null)
            {
                Stream tempStream = null;
                if (testResponse.Content != null)
                {
                    var baseStreamTemp = await testResponse.Content.ReadAsInputStreamAsync();
                    tempStream = baseStreamTemp.AsStreamForRead();
                    if (tempStream != null)
                    {
                        MemoryStream memoryStreamFile = new MemoryStream();
                        using (tempStream)
                        {
                            using (memoryStreamFile)
                            {
                                await tempStream.CopyToAsync(memoryStreamFile);
                            }
                            tempStream.Dispose();
                        }

                        byte[] resultInBytes;
                        resultInBytes = memoryStreamFile.ToArray();
                        var textRead = Encoding.UTF8.GetString(resultInBytes, 0, resultInBytes.Length);
                        if (textRead.Length > 0)
                        {
                            var currentAppVersion = GetAppVersion();
                            textRead = textRead.Trim();
                            updateState.newUpdate = !textRead.Equals(currentAppVersion);
                            updateState.updateVersion = textRead;
                            try
                            {
                                var textReadArray = textRead.Trim().Split('.');
                                var AppVersionNumberArray = currentAppVersion.Trim().Split('.');
                                if (int.Parse(textReadArray[0]) > int.Parse(AppVersionNumberArray[0]))
                                {
                                    updateState.newUpdate = true;
                                }
                                else if (int.Parse(textReadArray[0]) == int.Parse(AppVersionNumberArray[0]))
                                {
                                    if (int.Parse(textReadArray[1]) > int.Parse(AppVersionNumberArray[1]))
                                    {
                                        updateState.newUpdate = true;
                                    }
                                    else if (int.Parse(textReadArray[1]) == int.Parse(AppVersionNumberArray[1]))
                                    {
                                        if (int.Parse(textReadArray[2]) > int.Parse(AppVersionNumberArray[2]))
                                        {
                                            updateState.newUpdate = true;
                                        }
                                        else
                                        {
                                            updateState.newUpdate = false;
                                        }
                                    }
                                    else
                                    {
                                        updateState.newUpdate = false;
                                    }
                                }
                                else
                                {
                                    updateState.newUpdate = false;
                                }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                }
            }

            return updateState;
        }

        public static string GetAppVersion()
        {
            try
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion version = packageId.Version;

                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
            catch (Exception ex)
            {

            }
            return "1.0.0.0";
        }

        public static async Task<Windows.Web.Http.HttpResponseMessage> GetResponse(string url, CancellationToken cancellationToken, HttpCredentialsHeaderValue authenticationHeaderValue = null, bool returnResponseAnyway = false)
        {
            var _client = new Windows.Web.Http.HttpClient();
            if (authenticationHeaderValue != null)
            {
                _client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            }
            Windows.Web.Http.HttpResponseMessage response = null;
            try
            {
                Uri uri = null;
                try
                {
                    uri = new Uri(url);
                }
                catch (Exception e)
                {

                }
                if (uri != null)
                {
                    response = await _client.GetAsync(uri, Windows.Web.Http.HttpCompletionOption.ResponseHeadersRead).AsTask(cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        if (!returnResponseAnyway)
                        {
                            return null;
                        }
                        else
                        {
                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
    public class UpdateState
    {
        public bool newUpdate = false;
        public string updateVersion = "1.0.0.0";
        public UpdateState(bool newUpdate, string updateVersion)
        {
            this.newUpdate = newUpdate;
            this.updateVersion = updateVersion;
        }
    }
}
