using WMPLib;

namespace FinalProject
{
    internal static class Audio
    {
        readonly static WindowsMediaPlayer player = new();
        public static void LevelUpSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\LevelUp_Sound.mp3";
        }
        public static void LockedGateSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\Locked.mp3";
        }
        public static void PullLeverSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\Pull_Lever.mp3";
        }
        public static void Unlock_DoorSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\Unlock_Door.mp3";
        }
        public static void PurchaseSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\Buying_Sound.mp3";
        }
        public static void TreasureSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\TreasureChest_Sound.mp3";
        }
        public static void TrapSFX()
        {
            player.URL = Directory.GetCurrentDirectory() + "\\Traps_Sound.mp3";
        }
        public static void WinMusic()
        {
            player.settings.volume = 25;
            player.settings.setMode("loop", true);
            player.URL = Directory.GetCurrentDirectory() + "\\Victory.mp3";
        }
    }
}
