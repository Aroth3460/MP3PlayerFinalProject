namespace Mp3PlayerFinalProject
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for the MainWindow.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The media player.
        /// </summary>
        private IMediaPlayer mediaplayer;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Starts the view timer.
        /// </summary>
        public void BeginTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += this.TrackTimeCounter;
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Allows the user to play the local file they selected.
        /// </summary>
        /// <param name="sender">Sends the object.</param>
        /// <param name="e">Routes the event arguments.</param>
        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                this.mediaplayer.CurrentUserAction = UserActionState.Play;
                (this.mediaplayer as MediaPlayer).MediaCheck(this.mediaplayer.CurrentUserAction);
                if ((this.mediaplayer as MediaPlayer).Position == (this.mediaplayer as MediaPlayer).NaturalDuration.TimeSpan)
                {
                    (this.mediaplayer as MediaPlayer).Position = TimeSpan.Zero;
                    this.BeginTimer();
                }

                this.pauseBtn.Visibility = Visibility.Visible;
                this.stopBtn.Visibility = Visibility.Visible;
            }

            catch (InvalidOperationException ioe)
            {
                // Do nothing as the threads are still safe even with the operation exception.
            }
        }

        /// <summary>
        /// Allows the user to stop the application.
        /// </summary>
        /// <param name="sender">Sends the object.</param>
        /// <param name="e">The routed event arguments.</param>
        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mediaplayer.CurrentUserAction = UserActionState.Stop;
                (this.mediaplayer as MediaPlayer).MediaCheck(this.mediaplayer.CurrentUserAction);
                this.timerLabel.Content = "Nothing is playing...";
                this.playBtn.Visibility = Visibility.Visible;
                this.pauseBtn.Visibility = Visibility.Visible;
            }

            catch (InvalidOperationException ioe)
            {
                // Do nothing as the threads are still safe even with the operation exception.
            }
        }

        /// <summary>
        /// Allows a user to pause the media player.
        /// </summary>
        /// <param name="sender">Sends the object.</param>
        /// <param name="e">The routed event arguments.</param>
        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mediaplayer.CurrentUserAction = UserActionState.Pause;
                (this.mediaplayer as MediaPlayer).MediaCheck(UserActionState.Pause);
                this.playBtn.Visibility = Visibility.Visible;
                this.stopBtn.Visibility = Visibility.Visible;
            }
            catch (InvalidOperationException ioe)
            {
                // Do nothing as the threads are still safe even with the operation exception.
            }
        }

        /// <summary>
        /// Upon loading, set up the user media input.
        /// </summary>
        /// <param name="sender">Sends the object.</param>
        /// <param name="e">Routes the event arguments.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LockVisibility();
            this.mediaplayer = MediaPlayer.MediaPlayerInstance;
        }

        /// <summary>
        /// Tracks the counter, and alters the view content based on the state of the media player.
        /// </summary>
        /// <param name="sender">Sends the object.</param>
        /// <param name="e">The event arguments.</param>
        private void TrackTimeCounter(object sender, EventArgs e)
        {
            try
            {
                // If the source exists, alter the content of the media player.
                if ((this.mediaplayer as MediaPlayer).Source != null)
                {
                    this.timerLabel.Content = (this.mediaplayer as MediaPlayer).Position.ToString(@"mm\:ss") + "/ " + (this.mediaplayer as MediaPlayer).NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                    if (this.mediaplayer.CurrentUserAction == UserActionState.Pause) // Paused State
                    {
                        // Hide visibility and alter the timer label content.
                        this.pauseBtn.Visibility = Visibility.Hidden;
                        this.timerLabel.Content += ": PAUSED - Press Play to continue.";
                    }
                    else if (this.mediaplayer.CurrentUserAction == UserActionState.Play) // Play state.
                    {
                        // Only change content since the file must be playable after it has ended.
                        this.timerLabel.Content += ": PLAYING";
                    }
                    else if (this.mediaplayer.CurrentUserAction == UserActionState.Stop) // Stop state.
                    {
                        // Remove the stop & pause button visibility.
                        this.stopBtn.Visibility = Visibility.Hidden;
                        this.pauseBtn.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    // If no file is chosen, this will display.
                    this.timerLabel.Content = "Nothing is Playing...";
                }
            }
            catch (InvalidOperationException ioe)
            {
                // Do nothing as the threads are still safe even with the operation exception.
            }
        }

        /// <summary>
        /// Allows the user to browse for an audio file.
        /// </summary>
        /// <param name="sender">Sends the object.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Alter the media state to FILE OPEN.
                this.mediaplayer.CurrentUserAction = UserActionState.FileOpen;

                // Call the media check method to determine which media player method should be used.
                this.mediaplayer.MediaCheck(this.mediaplayer.CurrentUserAction);

                // Assign the media player file name.
                string condensedFileName = Path.GetFileName(this.mediaplayer.MediaFile.FileName);
                //this.FileName.Content = this.mediaplayer.MediaFile.FileName;
                this.FileName.Content = condensedFileName;

                // Begin the timer and unlock the button visibility.
                this.BeginTimer();
                this.UnlockVisibility();
            }
            catch (NullReferenceException nre)
            {
                // If an incorrect file or file type is chosen, display this error message to the user.
                MessageBox.Show("You must choose an MP3 or WAV file.");
            }
            catch (InvalidOperationException ioe)
            {
                // Do nothing as the threads are still safe even with the operation exception.
            }
        }

        /// <summary>
        /// Unlocks the button visibility.
        /// </summary>
        private void UnlockVisibility()
        {
            this.pauseBtn.Visibility = Visibility.Visible;
            this.playBtn.Visibility = Visibility.Visible;
            this.stopBtn.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Locks the button visibility.
        /// </summary>
        private void LockVisibility()
        {
            this.playBtn.Visibility = Visibility.Hidden;
            this.pauseBtn.Visibility = Visibility.Hidden;
            this.stopBtn.Visibility = Visibility.Hidden;
        }
    }
}
