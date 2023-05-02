public abstract class RabbitStateBase
{
    public abstract void EnterState(RabbitStateManager rabbit);
    public abstract void UpdateState(RabbitStateManager rabbit);
    public abstract void ExitState(RabbitStateManager rabbit);
}