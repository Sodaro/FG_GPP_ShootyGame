using System;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;
using PlayerLoopType = UnityEngine.PlayerLoop;

public class InputHandler : MonoBehaviour
{
    //Credits to Dmytro Ivanov for sharing a system for injecting EarlyUpdate
    private struct MyEarlyUpdateCallerTag
    {
    };

    private static InputHandler _instance;
    public static InputHandler Instance => _instance;
    public struct InputVars
    {
        public Vector2 moveInput;
        public Vector2 mouseInput;
        public bool jumpInput;
        public bool shootWasPressed;
        public bool shootIsHeld;
        public bool sprintIsHeld;
    }
    public InputVars Inputs { get; private set; }

    private void Start()
    {
        //inject earlyupdate to check for input before fixedupdate
        var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
        var initStepIndex = Array.FindIndex(playerLoop.subSystemList, x => x.type == typeof(PlayerLoopType.EarlyUpdate));
        if (initStepIndex >= 0)
        {
            var systems = playerLoop.subSystemList[initStepIndex].subSystemList;

            // Check if we're not already injected
            if (!systems.Select(x => x.type)
                .Contains(typeof(MyEarlyUpdateCallerTag)))
            {
                var systemsList = systems.ToList();
                systemsList.Add(new PlayerLoopSystem
                {
                    type = typeof(MyEarlyUpdateCallerTag),
                    updateDelegate = EarlyUpdate
                });
                playerLoop.subSystemList[initStepIndex].subSystemList = systemsList.ToArray();
                PlayerLoop.SetPlayerLoop(playerLoop);
            }
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void EarlyUpdate()
    {
        InputVars inputs;
        inputs.moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputs.mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        inputs.jumpInput = Input.GetButton("Jump");
        inputs.shootWasPressed = Input.GetMouseButtonDown(0);
        inputs.shootIsHeld = Input.GetMouseButton(0);
        inputs.sprintIsHeld = Input.GetKey(KeyCode.LeftShift);
        Inputs = inputs;
    }

}
