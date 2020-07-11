namespace Mp3PlayerFinalProject
{
    /// <summary>
    /// An enum used to determine the current state of the media player.
    /// </summary>
    public enum UserActionState
    {
        /// <summary>
        /// The state in which a file is being open.
        /// </summary>
        FileOpen,

        /// <summary>
        /// The state in which a file is being played.
        /// </summary>
        Play,

        /// <summary>
        /// The state in which a file is paused.
        /// </summary>
        Pause,

        /// <summary>
        /// The state in which a file has been stopped.
        /// </summary>
        Stop
    }
}