public abstract class BaseState
{
    public NewEnemy enemy;
    public StateMachine stateMachine;

    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();

}