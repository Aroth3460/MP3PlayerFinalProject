namespace Mp3PlayerFinalProject
{
    /// <summary>
    /// An interface used as a contract for all media players.
    /// </summary>
    public interface IMediaPlayer
    {
        /// <summary>
        /// Gets or sets the media file.
        /// </summary>
        IMediaFile MediaFile { get; set; }

        /// <summary>
        /// Gets or sets the current user action.
        /// </summary>
        UserActionState CurrentUserAction { get; set; }

        /// <summary>
        /// Checks and runs the appropriate media functionality based on the user action.
        /// </summary>
        /// <param name="userAction">The user action.</param>
        void MediaCheck(UserActionState userAction);
    }
}
