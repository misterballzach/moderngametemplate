namespace Lex.Sim
{
    /// <summary>
    /// Base class for status effects (e.g., buffs, debuffs).
    /// </summary>
    public abstract class Status
    {
        public Entity Target { get; }
        public int Duration { get; protected set; }

        protected Status(Entity target, int duration)
        {
            Target = target;
            Duration = duration;
        }

        /// <summary>
        /// Called when the status is first applied to the target.
        /// </summary>
        public virtual void OnApply(SimContext ctx) { }

        /// <summary>
        /// Called when the status is removed (either by expiring or being dispelled).
        /// </summary>
        public virtual void OnRemove(SimContext ctx) { }

        /// <summary>
        /// Called at the end of a turn. Default behavior is to tick down duration.
        /// </summary>
        public virtual void OnTurnEnd(SimContext ctx)
        {
            if (Duration > 0)
            {
                Duration--;
            }
        }

        public bool IsExpired => Duration == 0;
    }
}
