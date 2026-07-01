namespace AloneAIOR.Infrastructure.GameIntegration.Input.Providers
{
    /// <summary>
    /// Resolves configured logical action names into backend-neutral key codes.
    /// </summary>
    /// <remarks>
    /// Implementations may use different local configuration sources. Storage details remain private to the
    /// provider and must not leak into game logic.
    /// </remarks>
    public interface IKeyMappingProvider
    {
        /// <summary>
        /// Gets a value indicating whether mappings have been loaded successfully.
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Reloads mappings from the provider's configured source.
        /// </summary>
        void Reload();

        /// <summary>
        /// Attempts to resolve a logical action name.
        /// </summary>
        /// <param name="actionName">Stable logical action name.</param>
        /// <param name="keyCode">Receives the backend-neutral key code when resolution succeeds.</param>
        /// <returns><see langword="true"/> when a mapping exists; otherwise <see langword="false"/>.</returns>
        bool TryResolve(string actionName, out int keyCode);
    }
}
