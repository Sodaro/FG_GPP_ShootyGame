using UnityEngine;

public abstract class PlayerBaseComponent : MonoBehaviour
{
    #region Public Methods
    public virtual void Initialize() { }
    public virtual void OnUpdate(float delta) { }
    public virtual void OnUpdate(float delta, in InputHandler.InputVars inputs) { }
    public virtual void OnFixedUpdate(float fixedDelta, in InputHandler.InputVars inputs) { }
    #endregion
}
