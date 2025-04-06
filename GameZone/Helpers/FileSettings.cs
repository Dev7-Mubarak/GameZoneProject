namespace GameZone.Helpers
{
    public static class FileSettings
    {
        public const string GamesImagesPath = "/Assets/Images/GamesImages";
        public const string StationFilePath = "/Assets/Images/StationsImages";
        public const string DepositsFilePath = "/Assets/Images/DepositsImages";
        public const string RoomsFilePath = "/Assets/Images/RoomsImages";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
