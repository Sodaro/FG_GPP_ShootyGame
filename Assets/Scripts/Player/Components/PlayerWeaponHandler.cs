using UnityEngine;
using UnityEngine.AI;

public class PlayerWeaponHandler : PlayerBaseComponent
{
    #region Serialized Fields
    [SerializeField] private GameObject _muzzleFlashPrefab = null;
    [SerializeField] private Transform _muzzleTransform = null;
    #endregion

    #region Private Methods
    private Transform _cameraTransform;
    private PlayerInput _input;
    private int _entityLayerMask;
    #endregion
    #region Public Methods
    public override void C_Initialize(PlayerBaseComponent component)
    {
        _entityLayerMask = 1 << gameObject.layer;
        _cameraTransform = Camera.main.transform;
        _input = (PlayerInput)component;
    }

    public override void C_Update(float delta)
    {
        if (_input.inputs.shootWasPressed)
        {
            GameObject instance = Instantiate(_muzzleFlashPrefab, _muzzleTransform);
            Destroy(instance, 0.1f);
        }
        
    }
    public override void C_FixedUpdate(float fixedDelta)
    {
        if (_input.inputs.shootWasPressed)
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, 100f, _entityLayerMask))
            {
                Rigidbody body = hit.collider.attachedRigidbody;
                Transform bodyParent = hit.collider.attachedRigidbody.transform.parent;
                if (body == null || bodyParent == null)
                    return;

                if (hit.collider.attachedRigidbody.transform.parent.TryGetComponent(out Enemy enemy))
                {
                    enemy.Kill((transform.position - hit.point).normalized);
                }
            }
        }
    }
    #endregion
}
