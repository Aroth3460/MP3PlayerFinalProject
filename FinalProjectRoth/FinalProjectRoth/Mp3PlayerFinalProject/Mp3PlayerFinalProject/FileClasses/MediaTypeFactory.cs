namespace Mp3PlayerFinalProject
{
    /// <summary>
    /// A static class used to generate a media file based on the given type.
    /// </summary>
    public static class MediaTypeFactory
    {
        /// <summary>
        /// A method used to create a media file based on the given type.
        /// </summary>
        /// <param name="fileType">The file type.</param>
        /// <returns>The correct file based on the file type.</returns>
        public static IMediaFile MediaFileFactory(FileType fileType)
        {
            IMediaFile mediaFile;
            switch (fileType)
            {
                case FileType.Mp3:
                    mediaFile = new Mp3File();
                    break;
                case FileType.Wav:
                    mediaFile = new WavFile();
                    break;
                default:
                    mediaFile = null;
                    break;
            }

            return mediaFile;
        }
    }
}
