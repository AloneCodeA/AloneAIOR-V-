namespace AloneAIOR.GameLogic.Functions.Map.Scripting.Contracts
{
    /// <summary>
    /// Loads and controls one route execution context.
    /// </summary>
    /// <remarks>
    /// Route storage and protected document handling remain outside this public contract.
    /// </remarks>
    public interface IRouteExecutor
    {
        /// <summary>
        /// Gets a value indicating whether a route has been loaded and validated.
        /// </summary>
        bool IsRouteLoaded { get; }

        /// <summary>
        /// Gets a value indicating whether route execution is active.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Attempts to load a route by its logical name.
        /// </summary>
        /// <param name="routeName">Stable logical name of the route.</param>
        /// <returns><see langword="true"/> when the route is accepted; otherwise <see langword="false"/>.</returns>
        bool TryLoad(string routeName);

        /// <summary>
        /// Attempts to start the loaded route.
        /// </summary>
        /// <returns><see langword="true"/> when execution starts; otherwise <see langword="false"/>.</returns>
        bool TryStart();

        /// <summary>
        /// Requests a controlled stop and release of route-owned actions.
        /// </summary>
        void Stop();
    }
}
