using UnityEngine;

public abstract class PlayerBaseComponent : MonoBehaviour
{
    #region Public Methods
    public virtual void C_Initialize() { }
    public virtual void C_Initialize(PlayerBaseComponent component) { }
    public virtual void C_Update(float delta) { }
    public virtual void C_Update() { }
    public virtual void C_FixedUpdate(float fixedDelta) { }
    #endregion
}
