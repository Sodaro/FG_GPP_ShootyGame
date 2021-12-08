public class EnemyFSM
{
    #region Public Methods And Properties
    public EnemyFSM(EnemyState initialState, Enemy enemy)
    {
        CurrentState = initialState;
        CurrentState.Enter(enemy);
    }
    public EnemyState CurrentState { get; private set; }
    public void ChangeState(EnemyState newState, Enemy enemy)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter(enemy);
    }

    public void Update()
    {
        if (CurrentState == null)
            return;

        CurrentState.Update();
    }
    #endregion
}
