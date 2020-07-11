namespace Mp3PlayerFinalProject
{
    using System;
    using System.Text.RegularExpressions;
    using Microsoft.Win32;

    /// <summary>
    /// A class used to represent a media player. Only one instance of this player can be created, A singleton if you will.
    /// </summary>
    public sealed class MediaPlayer : System.Windows.Media.MediaPlayer, IMediaPlayer
    {
        /// <summary>
        /// The private media player instance. It's instantiated at it's definition in order to implement the singleton design pattern.
        /// </summary>
        private static MediaPlayer mediaPlayerInstance = new MediaPlayer();

        /// <summary>
        /// Prevents a default instance of the MediaPlayer class from being created.
        /// </summary>
        private MediaPlayer()
        {
        }

        /// <summary>
        /// Gets the current instance. If it exists, no new instance will be instantiated.
        /// </summary>
        public static MediaPlayer MediaPlayerInstance
        {
            get
            {
                return mediaPlayerInstance;
            }
        }

        /// <summary>
        /// Gets or sets the current media file.
        /// </summary>
        public IMediaFile MediaFile { get; set; }

        /// <summary>
        /// Gets or sets the user's current action.
        /// </summary>
        UserActionState IMediaPlayer.CurrentUserAction { get; set; }

        /// <summary>
        /// Gets the general file filter.
        /// </summary>
        public string GeneralFilter { get => "All Media Files|*.wav;*.mp3;"; }

        /// <summary>
        /// A method that calls a specific method based on the user's button inputs.
        /// </summary>
        /// <param name="userAction">The button the user pressed.</param>
        public void MediaCheck(UserActionState userAction)
        {
            switch (userAction)
            {
                case UserActionState.Play:
                    this.PlayMedia();
                    break;
                case UserActionState.Pause:
                    this.PauseMedia();
                    break;
                case UserActionState.Stop:
                    this.StopMedia();
                    break;
                case UserActionState.FileOpen:
                    this.OpenMedia();
                    break;
            }
        }

        /// <summary>
        /// Allows the user to pause the media.
        /// </summary>
        private void PauseMedia()
        {
            mediaPlayerInstance.Pause();
        }

        /// <summary>
        /// Allows the user to play the media.
        /// </summary>
        private void PlayMedia()
        {
            mediaPlayerInstance.Play();
        }

        /// <summary>
        /// Allows the user to stop the media player.
        /// </summary>
        private void StopMedia()
        {
            mediaPlayerInstance.Stop();
        }

        /// <summary>
        /// Allows the user to browse for an audio file.
        /// </summary>
        private void OpenMedia()
        {
            // If a media player instance exists, generate an open file dialog that uses the general filter.
            if (mediaPlayerInstance != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = this.GeneralFilter;
                if (openFileDialog.ShowDialog() == true)
                {
                    // If the user chose a correct file open the file and set the media file properties.
                    mediaPlayerInstance.Open(new Uri(openFileDialog.FileName));
                    this.MediaFileSet(openFileDialog.FileName);
                    this.MediaFile.SetName(openFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Generates and sets the media file based on the type of file the user chose.
        /// </summary>
        /// <param name="filename">The file name.</param>
        private void MediaFileSet(string filename)
        {
            Regex regexMp3 = new Regex(@"^.*\.(mp3|MP3)$");
            Regex regexWav = new Regex(@"^.*\.(wav|WAV)$");

            if (filename != null)
            {
                if (regexMp3.IsMatch(filename))
                {
                    this.MediaFile = MediaTypeFactory.MediaFileFactory(FileType.Mp3);
                }
                else if (regexWav.IsMatch(filename))
                {
                    this.MediaFile = MediaTypeFactory.MediaFileFactory(FileType.Wav);
                }
            }
        }
    }
}
