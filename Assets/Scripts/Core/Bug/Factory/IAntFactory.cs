namespace Core.Bug.Factory
{
    public interface IAntFactory
    {
        public BugController CreateWorkerBug();
        public BugController CreatePredatorBug();
        public void DestroyBug(BugController bug);
        public bool IsWorker(BugController bug);
        public bool IsPredator(BugController bug);
    }
}