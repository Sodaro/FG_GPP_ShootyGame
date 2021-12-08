using UnityEngine;

public class PlayerMouseLook : PlayerBaseComponent
{
    #region Serialized Fields
    [SerializeField] private float _rotationSpeed = 100f;
    #endregion

    #region Private Fields
    private Transform _cameraTransform;
    private Transform _playerTransform;
    #endregion

    #region Public Methods
    public override void Initialize()
    {
        _cameraTransform = Camera.main.transform;
        _playerTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnUpdate(float delta, in InputHandler.InputVars inputs)
    {
        Vector2 mouseInput = inputs.mouseInput;
        if (mouseInput == Vector2.zero)
            return;
        _playerTransform.rotation *= Quaternion.AngleAxis(mouseInput.x * _rotationSpeed * delta, Vector3.up);
        Vector3 localEuler = _cameraTransform.localRotation.eulerAngles;

        float xRot = localEuler.x;
        if (xRot > 180)
            xRot -= 360;

        if ((xRot < 85f && mouseInput.y < 0) || (xRot > -85f && mouseInput.y > 0))
        {
            _cameraTransform.localRotation *= Quaternion.AngleAxis(-mouseInput.y * _rotationSpeed * delta, Vector3.right);
        }
    }
    #endregion
}
