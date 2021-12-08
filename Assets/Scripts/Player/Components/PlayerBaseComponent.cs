using UnityEngine;

public abstract class PlayerBaseComponent : MonoBehaviour
{
    public bool dirtyFlag = false;
    #region Public Methods
    public virtual void Initialize() { }
    public virtual void Initialize(PlayerBaseComponent component) { }
    public virtual void OnUpdate(float delta) { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate(float fixedDelta) { }
    #endregion
}
