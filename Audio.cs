using FinalProject.Menus;
using WMPLib;

namespace FinalProject
{
    internal static class Audio
    {
        readonly static WindowsMediaPlayer _player = new();
        readonly static string _path = MainMenu.Path + "\\Audio\\";
        public static void LevelUpSFX()
        {
            _player.URL = _path + "LevelUp_Sound.mp3";
        }
        public static void LockedGateSFX()
        {
            _player.URL = _path + "Locked.mp3";
        }
        public static void PullLeverSFX()
        {
            _player.URL = _path + "Pull_Lever.mp3";
        }
        public static void Unlock_DoorSFX()
        {
            _player.URL = _path + "Unlock_Door.mp3";
        }
        public static void PurchaseSFX()
        {
            _player.URL = _path + "Buying_Sound.mp3";
        }
        public static void TreasureSFX()
        {
            _player.URL = _path + "TreasureChest_Sound.mp3";
        }
        public static void TrapSFX()
        {
            _player.URL = _path + "Traps_Sound.mp3";
        }
        public static void WinMusic()
        {
            _player.settings.volume = 25;
            _player.settings.setMode("loop", true);
            _player.URL = _path + "Victory.mp3";
        }
    }
}
