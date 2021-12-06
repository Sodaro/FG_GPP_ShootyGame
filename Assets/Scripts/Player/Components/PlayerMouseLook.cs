using UnityEngine;

public class PlayerMouseLook : PlayerBaseComponent
{
    #region Serialized Fields
    [SerializeField] private float _rotationSpeed = 100f;
    #endregion

    #region Private Fields
    private Transform _cameraTransform;
    private Transform _playerTransform;
    private PlayerInput _input;
    #endregion

    #region Public Methods
    public override void C_Initialize(PlayerBaseComponent playerInput)
    {
        _input = (PlayerInput)playerInput;
        _cameraTransform = Camera.main.transform;
        _playerTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void C_Update(float delta)
    {
        Vector3 mouseInput = _input.inputs.mouseInput;
        _playerTransform.rotation *= Quaternion.AngleAxis(mouseInput.x * _rotationSpeed * delta, Vector3.up);
        _cameraTransform.localRotation *= Quaternion.AngleAxis(-mouseInput.y * _rotationSpeed * delta, Vector3.right);
    }
    #endregion
}
