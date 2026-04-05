using Views.Bug;

public abstract class BaseAntFactory
{
    public abstract BugController CreateWorkerBug();
    public abstract BugController CreatePredatorBug();
}

