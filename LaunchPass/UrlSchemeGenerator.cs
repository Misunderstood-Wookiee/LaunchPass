using System;

namespace RetroPass
{
    internal class UrlSchemeGenerator
    {
        public static string GetUrl(Game game)
        {
            string url;
            switch (game.GamePlatform.EmulatorType)
            {
                case Platform.EEmulatorType.retroarch:
                    url = GetUrlRetroarch(game);
                    break;

                case Platform.EEmulatorType.rgx:
                    url = GetUrlRetrix(game);
                    break;

                case Platform.EEmulatorType.xbsx2:
                    url = GetUrlXBSX2(game);
                    break;

                case Platform.EEmulatorType.dolphin:
                    url = GetUrlDolphin(game);
                    break;

                case Platform.EEmulatorType.ppsspp:
                    url = GetUrlPpsspp(game);
                    break;

                case Platform.EEmulatorType.duckstation:
                    url = GetUrlDuckstation(game);
                    break;

                case Platform.EEmulatorType.flycast:
                    url = GetUrlFlycast(game);
                    break;

                case Platform.EEmulatorType.xenia:
                    url = GetUrlXenia(game);
                    break;

                case Platform.EEmulatorType.xeniacanary:
                    url = GetUrlXeniaCanary(game);
                    break;

                case Platform.EEmulatorType.gamecube:
                    url = GetUrlgamecube(game);
                    break;

                case Platform.EEmulatorType.wii:
                    url = GetUrlwii(game);
                    break;

                case Platform.EEmulatorType.virtualconsole:
                    url = GetUrlvirtualconsole(game);
                    break;

                case Platform.EEmulatorType.riivolution:
                    url = GetUrlriivolution(game);
                    break;

                case Platform.EEmulatorType.nintendo64:
                    url = GetUrlnintendo64(game);
                    break;

                case Platform.EEmulatorType.gameboy:
                    url = GetUrlgameboy(game);
                    break;

                case Platform.EEmulatorType.gbcolour:
                    url = GetUrlgbcolour(game);
                    break;

                case Platform.EEmulatorType.gbadvance:
                    url = GetUrlgbadvance(game);
                    break;

                case Platform.EEmulatorType.win95:
                    url = GetUrlwin95(game);
                    break;

                case Platform.EEmulatorType.w98:
                    url = GetUrlw98(game);
                    break;

                case Platform.EEmulatorType.saturn:
                    url = GetUrlsaturn(game);
                    break;

                case Platform.EEmulatorType.nds:
                    url = GetUrlnds(game);
                    break;

                case Platform.EEmulatorType.dos:
                    url = GetUrldos(game);
                    break;

                default:
                    // Handle default case and throw an exception for unsupported emulator type.
                    throw new InvalidOperationException("Unsupported emulator type!");
            }

            return url;
        }

        private static string GetUrlRetroarch(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlnintendo64(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlgameboy(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlgbcolour(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlgbadvance(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlwin95(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlw98(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlsaturn(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlnds(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrldos(Game game)
        {
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlRetrix(Game game)
        {
            //retrix uses the same Uri scheme syntax as retroarch
            string args = "cmd=" + "retroarch";
            args += " -L";
            args += " cores\\" + game.CoreName;
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += " &launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlXBSX2(Game game)
        {
            string args = "cmd=" + "xbsx2.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlDolphin(Game game)
        {
            string args = "cmd=" + "dolphin.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlgamecube(Game game)
        {
            string args = "cmd=" + "dolphin.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlwii(Game game)
        {
            string args = "cmd=" + "dolphin.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlvirtualconsole(Game game)
        {
            string args = "cmd=" + "dolphin.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlriivolution(Game game)
        {
            string args = "cmd=" + "dolphin.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlPpsspp(Game game)
        {
            string args = "cmd=" + "ppsspp.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlDuckstation(Game game)
        {
            string args = "cmd=" + "duckstation.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlFlycast(Game game)
        {
            string args = "cmd=" + "flycast.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlXenia(Game game)
        {
            string args = "cmd=" + "xenia.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }

        private static string GetUrlXeniaCanary(Game game)
        {
            string args = "cmd=" + "xeniacanary.exe";
            args += " \"" + Uri.EscapeDataString(game.ApplicationPathFull) + "\"";
            args += "&launchOnExit=" + "LaunchPass:";
            return game.GamePlatform.EmulatorType.ToString() + ":?" + args;
        }
    }
}