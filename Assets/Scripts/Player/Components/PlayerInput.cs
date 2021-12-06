using UnityEngine;

public class PlayerInput : PlayerBaseComponent
{ 
    public struct Inputs
    {
        public Vector2 moveInput;
        public Vector2 mouseInput;
        public bool jumpInput;
        public bool shootWasPressed;
        public bool shootIsHeld;
        public bool sprintIsHeld;
    }
    public override void C_Update()
    {
        inputs.moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        inputs.mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        inputs.jumpInput = Input.GetButton("Jump");
        inputs.shootWasPressed = Input.GetMouseButtonDown(0);
        inputs.shootIsHeld = Input.GetMouseButton(0);
        inputs.sprintIsHeld = Input.GetKey(KeyCode.LeftShift);
    }
    public Inputs inputs;
}
