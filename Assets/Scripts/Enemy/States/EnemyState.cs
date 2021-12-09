using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyState
{
    protected Enemy Owner;
    protected Transform Target;
    protected NavMeshAgent Agent;

    #region Public Methods
    public virtual void Enter(Enemy owner)
    {
        Owner = owner;
    }
    public virtual void Exit() { }
    public virtual void Update() { }

    #endregion
}
