using UnityEngine;
using Observer;
[RequireComponent(typeof(PlayerInput), typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerWeaponHandler), typeof(PlayerMouseLook))]
public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private PlayerInput _input;
    private PlayerPhysics _physics;
    private PlayerMouseLook _mouseLook;
    private PlayerWeaponHandler _weaponHandler;

    #endregion
    #region Private Methods
    private void Awake()
    {
        _input = gameObject.AddComponent<PlayerInput>();
        _physics = GetComponent<PlayerPhysics>();
        _mouseLook = GetComponent<PlayerMouseLook>();
        _weaponHandler = GetComponent<PlayerWeaponHandler>();
    }
    private void OnEnable()
    {
        ServiceLocator.Current.Get<EventHandler>().onEnemyDeath += PlayerController_onEnemyDeath;
    }

    private void OnDisable()
    {
        ServiceLocator.Current.Get<EventHandler>().onEnemyDeath -= PlayerController_onEnemyDeath;
    }

    private void PlayerController_onEnemyDeath()
    {
        Debug.Log("Enemy died!");
    }

    private void Start()
    {
        _physics.C_Initialize(_input);
        _mouseLook.C_Initialize(_input);
        _weaponHandler.C_Initialize(_input);
        
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        _input.C_Update();
        _mouseLook.C_Update(delta: dt);
        _weaponHandler.C_Update(delta: dt);
    }

    private void FixedUpdate()
    {
        float fdt = Time.fixedDeltaTime;
        _physics.C_FixedUpdate(fixedDelta: fdt);
        _weaponHandler.C_FixedUpdate(fixedDelta: fdt);
    }
    #endregion
}
