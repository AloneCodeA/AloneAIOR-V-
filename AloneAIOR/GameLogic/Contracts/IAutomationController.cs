namespace AloneAIOR.GameLogic.Contracts
{
    /// <summary>
    /// Controls the lifecycle of one automation session without exposing runtime implementation details.
    /// </summary>
    /// <remarks>
    /// Implementations own session state transitions and terminal cleanup. Callers should not start a second
    /// session while <see cref="IsRunning"/> is <see langword="true"/>.
    /// </remarks>
    public interface IAutomationController
    {
        /// <summary>
        /// Gets a value indicating whether an automation session is active.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets a value indicating whether the active session is paused.
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// Attempts to start an automation mode.
        /// </summary>
        /// <param name="modeName">Stable logical name of the requested mode.</param>
        /// <returns><see langword="true"/> when the session starts; otherwise <see langword="false"/>.</returns>
        bool TryStart(string modeName);

        /// <summary>
        /// Attempts to pause the active session at a safe execution boundary.
        /// </summary>
        /// <returns><see langword="true"/> when the session is paused; otherwise <see langword="false"/>.</returns>
        bool TryPause();

        /// <summary>
        /// Attempts to resume a paused session.
        /// </summary>
        /// <returns><see langword="true"/> when execution resumes; otherwise <see langword="false"/>.</returns>
        bool TryResume();

        /// <summary>
        /// Requests a controlled stop and release of session-owned actions.
        /// </summary>
        void Stop();
    }
}
