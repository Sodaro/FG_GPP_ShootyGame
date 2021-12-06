using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _owner;
    #region Public Methods
    public virtual void Enter(Enemy enemy)
    {
        Debug.Log($"Entered State: {GetType().Name}");
        _owner = enemy;
    }
    public virtual void Exit()
    {
        Debug.Log($"Exited State: {GetType().Name}");
    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }
    #endregion
}
