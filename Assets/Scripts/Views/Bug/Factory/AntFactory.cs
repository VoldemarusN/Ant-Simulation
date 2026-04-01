public class AntFactory : BaseAntFactory
{
    public AntFactory()
    {
        
    }
    
    public override WorkerBug CreateWorkerBug()
    {
        throw new System.NotImplementedException();
    }

    public override PredatorBug CreatePredatorBug()
    {
        throw new System.NotImplementedException();
    }
}