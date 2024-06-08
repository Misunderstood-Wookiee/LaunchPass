using System.Xml.Serialization;
using Windows.Storage;

namespace RetroPass
{
    public class Platform
    {
        public enum EEmulatorType
        {
            retroarch,
            rgx,
            xbsx2,
            dolphin,
            flycast,
            ppsspp,
            duckstation,
            xenia,
            xeniacanary,
            gamecube,
            wii,
            virtualconsole,
            riivolution,
            nintendo64,
            gameboy,
            gbcolour,
            gbadvance,
            win95,
            w98,
            saturn,
            nds,
            dos,
        }

        public string Name { get; set; }
        public string SourceName { get; set; }
        public EEmulatorType EmulatorType { get; set; }
        public string BoxFrontPath { get; set; }
        public string ScreenshotGameTitlePath { get; set; }
        public string ScreenshotGameplayPath { get; set; }
        public string ScreenshotGameSelectPath { get; set; }
        public string VideoPath { get; set; }

        [XmlIgnoreAttribute]
        public StorageFolder BoxFrontFolder { get; set; }

        public void SetEmulatorType(string emulatorPath)
        {
            if (!string.IsNullOrEmpty(emulatorPath) &&
                    (emulatorPath.Contains("pcsx2", System.StringComparison.CurrentCultureIgnoreCase) ||
                    emulatorPath.Contains("xbsx2", System.StringComparison.CurrentCultureIgnoreCase
                    ))
                )
            {
                EmulatorType = EEmulatorType.xbsx2;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("flycast", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.flycast;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("retrix", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.rgx;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("dolphin", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.dolphin;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("ppsspp", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.ppsspp;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("duckstation", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.duckstation;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("gamecube", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.gamecube;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("wii", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.wii;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("virtualconsole", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.virtualconsole;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("riivolution", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.riivolution;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("nintendo64", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.nintendo64;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("gameboy", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.gameboy;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("gbcolour", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.gbcolour;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("gbadvance", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.gbadvance;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("win95", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.win95;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("w98", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.w98;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("saturn", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.saturn;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("nds", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.nds;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("dos", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.dos;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) &&
                (emulatorPath.Contains("xenia-canary", System.StringComparison.CurrentCultureIgnoreCase) ||
                emulatorPath.Contains("xeniacanary", System.StringComparison.CurrentCultureIgnoreCase) ||
                emulatorPath.Contains("xenia_canary", System.StringComparison.CurrentCultureIgnoreCase))
                )
            {
                EmulatorType = EEmulatorType.xeniacanary;
            }
            else if (!string.IsNullOrEmpty(emulatorPath) && emulatorPath.Contains("xenia", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EmulatorType = EEmulatorType.xenia;
            }
            else
            {
                //let it just be default retroarch
                EmulatorType = EEmulatorType.retroarch;
            }
        }

        public Platform Copy()
        {
            return (Platform)this.MemberwiseClone();
        }
    }
}