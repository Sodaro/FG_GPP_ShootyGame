using UnityEngine;

public class PlayerPhysics : PlayerBaseComponent
{
    #region Serialized Fields
    [SerializeField] private float _sprintSpeed = 7f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpSpeed = 5f;
    #endregion

    #region Private Fields
    private PlayerInput _input;
    private Vector3 _velocity;
    private Vector3 _moveDirection;
    private bool _isGrounded;
    private bool _isColliding;
    private int _layerMask;
    #endregion

    #region Public Methods
    public override void C_Initialize(PlayerBaseComponent input)
    {
        _input = (PlayerInput)input;
        _layerMask = 1;
    }
    public override void C_FixedUpdate(float fixedDelta)
    {
        float speed = _input.inputs.sprintIsHeld ? _sprintSpeed : _moveSpeed;
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, _layerMask, QueryTriggerInteraction.Ignore);
        _isColliding = Physics.Raycast(transform.position, _moveDirection,
            speed * fixedDelta + 0.5f, _layerMask, QueryTriggerInteraction.Ignore);

        Vector2 inputDir = _input.inputs.moveInput;
        _moveDirection = (inputDir.x * transform.right + inputDir.y * transform.forward).normalized;
        Vector3 inputVelocity = _moveDirection * speed;
        _velocity = new Vector3(inputVelocity.x, _velocity.y, inputVelocity.z);
        if (_isGrounded)
        {
            _velocity.y = _input.inputs.jumpInput ? _jumpSpeed : 0;
        }
        else
            _velocity.y += Physics.gravity.y * fixedDelta;

        if (!_isColliding)
            transform.position += _velocity * fixedDelta;
        else
        {
            transform.position += new Vector3(0, _velocity.y, 0) * fixedDelta;
        }
    }
    #endregion
}
