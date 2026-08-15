namespace AloneAIOR.Infrastructure.GameIntegration.Input.Backends
{
    /// <summary>
    /// Dispatches logical action state through one input backend.
    /// </summary>
    /// <remarks>
    /// The contract deliberately excludes device reports, native resources, and transport-specific state.
    /// Implementations must release owned actions when execution stops or the backend closes.
    /// </remarks>
    public interface IInputBackend
    {
        /// <summary>
        /// Gets a value indicating whether the backend can currently accept actions.
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// Attempts to change the pressed state of a logical action.
        /// </summary>
        /// <param name="actionName">Configured logical action name.</param>
        /// <param name="isPressed"><see langword="true"/> to press the action; <see langword="false"/> to release it.</param>
        /// <returns><see langword="true"/> when the state is accepted; otherwise <see langword="false"/>.</returns>
        bool TrySetActionState(string actionName, bool isPressed);

        /// <summary>
        /// Releases all actions owned by this backend.
        /// </summary>
        void ReleaseAllActions();

        /// <summary>
        /// Closes backend resources after releasing owned actions.
        /// </summary>
        void Close();
    }
}
