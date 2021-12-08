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

    public override void Update()
    {
        base.Update();
        if (_owner.HasTarget == false)
            return;

        float? dist = _owner.DistanceToTarget;
        if (dist == null)
            return;

        if (_owner.CheckTargetPathValididty() == false)
            return;

        if (dist < 50f)
        {
            _owner.SetHostile();
        }
    }
    #endregion
}
