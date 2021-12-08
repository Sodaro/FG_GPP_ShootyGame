using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _owner;
    #region Public Methods
    public virtual void Enter(Enemy enemy)
    {
        _owner = enemy;
    }
    public virtual void Exit()
    { 
    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }
    #endregion
}
