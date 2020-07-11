namespace Mp3PlayerFinalProject
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A class used to represent a WAV file.
    /// </summary>
    public class WavFile : IMediaFile
    {
        /// <summary>
        /// Initializes a new instance of the WavFile class.
        /// </summary>
        public WavFile()
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
        public Type TypeOfFile { get;  private set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Configures the files properties.
        /// </summary>
        public void ConfigureFile()
        {
            // Set the file type and filter.
            this.TypeOfFile = this.GetType();
            this.FileFilter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
        }

        /// <summary>
        /// Sets the name of the file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public void SetName(string fileName)
        {
            Regex regexWav = new Regex(@"^.*\.(wav|WAV)$");
            if (fileName != null && regexWav.IsMatch(fileName))
            {
                this.FileName = fileName;
            }
        }
    }
}
