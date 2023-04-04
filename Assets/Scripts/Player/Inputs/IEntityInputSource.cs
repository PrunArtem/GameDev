namespace Player
{
    interface IEntityInputSource
    {
        float Direction { get; }
        bool Jump { get; }
        bool Attack { get; }

        void ResetOneTimeAction();
    }
}
