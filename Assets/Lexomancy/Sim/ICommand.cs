namespace Lex.Sim
{
    /// <summary>
    /// Represents a single, atomic, and deterministic action that can be executed
    /// to modify the state of the simulation.
    /// </summary>
    /// <remarks>
    /// The SimContext will be created later, but is the intended parameter for Execute.
    /// </remarks>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command, modifying the simulation state.
        /// </summary>
        /// <param name="ctx">The simulation context, providing access to the game state.</param>
        void Execute(SimContext ctx);
    }
}
