namespace Mp3PlayerFinalProject
{
    using System;

    /// <summary>
    /// An inter
    /// </summary>
    public interface IMediaFile
    {
        /// <summary>
        /// Gets the filter for the necessary file type.
        /// </summary>
        string FileFilter { get; }

        /// <summary>
        /// Gets the type of file being read.
        /// </summary>
        Type TypeOfFile { get; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Configures the file dialog.
        /// </summary>
        void ConfigureFile();

        /// <summary>
        /// Sets the name of the media file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        void SetName(string fileName);
    }
}
