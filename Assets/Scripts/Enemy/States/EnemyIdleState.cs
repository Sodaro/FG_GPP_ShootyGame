public class EnemyIdleState : EnemyState
{
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
        if (Owner.HasTarget == false)
            return;

        float? dist = Owner.DistanceToTarget;
        if (dist == null)
            return;

        if (Owner.CheckTargetPathValididty() == false)
            return;

        if (dist < 50f)
        {
            Owner.SetHostile();
        }
    }
    #endregion
}
