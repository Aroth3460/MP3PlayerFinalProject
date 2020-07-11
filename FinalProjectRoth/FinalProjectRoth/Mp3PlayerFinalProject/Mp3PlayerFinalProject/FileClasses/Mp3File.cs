namespace Mp3PlayerFinalProject
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A class used to represent an MP3 file.
    /// </summary>
    public class Mp3File : IMediaFile
    {
        /// <summary>
        /// Initializes a new instance of the Mp3File class.
        /// </summary>
        public Mp3File()
        {
            this.ConfigureFile();
        }
        
        /// <summary>
        /// Gets the file filter.
        /// </summary>
        public string FileFilter { get; private set; }

        /// <summary>
        /// Gets the actual object type of the file.
        /// </summary>
        public Type TypeOfFile { get; private set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Configures the file's type and filter.
        /// </summary>
        public void ConfigureFile()
        {
            // Set the file type and filter.
            this.TypeOfFile = this.GetType();
            this.FileFilter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
        }

        /// <summary>
        /// Sets the name of the file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public void SetName(string fileName)
        {
            Regex regexMp3 = new Regex(@"^.*\.(mp3|MP3)$");
            if (fileName != null && regexMp3.IsMatch(fileName))
            {
                this.FileName = fileName;
            }
        }
    }
}
