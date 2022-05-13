namespace ExtraAssets.Scripts.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void ChangeState<TState>() where TState : IState;
    }
}