using UnityEngine;

public class EnemyHostileState : EnemyState
{
    #region Serialized Fields

    #endregion
    #region Private Fields
    private float _destinationUpdateTimer = 0;
    private float _destinationUpdateDelay = 0.5f;
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
        _owner.AlignWithSurface();
    }

    public override void Update()
    {
        base.Update();
        _destinationUpdateTimer += Time.deltaTime;
        if (_destinationUpdateTimer >= _destinationUpdateDelay)
        {
            _owner.UpdateDestination();
            _destinationUpdateTimer = 0;
        }
    }
    #endregion
}
