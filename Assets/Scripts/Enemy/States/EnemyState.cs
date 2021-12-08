using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyState
{
    protected Enemy _owner;
    protected Transform _target;
    protected NavMeshAgent _agent;

    #region Public Methods
    public virtual void Enter(Enemy owner)
    {
        _owner = owner;
    }
    public virtual void Exit() { }
    public virtual void Update() { }

    #endregion
}
