namespace Core.Bug.Behaviors
{
    public interface IBugBehavior : System.IDisposable
    {
        void Initialize(BugController owner);
    }
}
