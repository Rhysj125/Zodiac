namespace Assets.Scripts.Settings
{
    public static class PlayerSettings
    {
        public static float MusicVolume { get => musicVolume; set => musicVolume = value; }
        private static float musicVolume;

        public static bool EnableMusic { get => enableMusic; set => enableMusic = value; }
        private static bool enableMusic;
    }
}
