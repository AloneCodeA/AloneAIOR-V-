namespace AloneAIOR.Infrastructure.Vision.Services
{
    /// <summary>
    /// Provides named visual-state evidence without exposing frame acquisition internals.
    /// </summary>
    /// <remarks>
    /// Confidence values are normalized integers so callers do not depend on image or OCR implementation types.
    /// </remarks>
    public interface IVisionService
    {
        /// <summary>
        /// Gets a value indicating whether visual evidence can currently be evaluated.
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// Attempts to evaluate evidence for a named state.
        /// </summary>
        /// <param name="stateName">Stable logical name of the state to evaluate.</param>
        /// <param name="confidence">Receives a normalized confidence value from 0 through 100.</param>
        /// <returns><see langword="true"/> when evidence was evaluated; otherwise <see langword="false"/>.</returns>
        bool TryGetStateEvidence(string stateName, out int confidence);

        /// <summary>
        /// Resets accumulated capability-health state after recovery or reinitialization.
        /// </summary>
        void ResetHealth();
    }
}
