public class EnemyIdleState : EnemyState
{
    #region Serialized Fields

    #endregion
    #region Private Fields

    #endregion
    #region Private Methods

    #endregion
    #region Public Methods
    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (_owner.HasTarget == false)
            return;

        if (_owner.CheckTargetPathValididty() == false)
            return;

        if (_owner.DistanceToTarget < 50f)
        {
            _owner.SetHostile();
        }
    }
    #endregion
}
