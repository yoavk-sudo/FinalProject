using System.Media;

namespace FinalProject
{
    internal static class Audio
    {
        static SoundPlayer sound = new SoundPlayer();
        static public void playAudio(string fileName)
        {
            sound.SoundLocation = fileName;
            sound.Play();
        }
    }
}
