using UnityEngine;

public class EnemyHostileState : EnemyState
{
    #region Serialized Fields

    #endregion
    #region Private Fields
    private const int MAX_RETRY_COUNT = 3;
    private float _destinationUpdateTimer = 0;
    private float _destinationUpdateDelay = 0.5f;
    private int _retryCount = 0;
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
            bool success = _owner.CheckTargetPathValididty();
            if (!success)
            {
                _retryCount++;
                if (_retryCount >= MAX_RETRY_COUNT)
                {
                    _owner.ReturnToStart();
                }
            }
            else
            {
                _retryCount = 0;
                _owner.UpdateDestination();
            }
                
            _destinationUpdateTimer = 0;
        }
    }
    #endregion
}
