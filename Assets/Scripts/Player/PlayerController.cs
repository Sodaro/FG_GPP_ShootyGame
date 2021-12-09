using Observer;
using UnityEngine;
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerWeaponHandler), typeof(PlayerMouseLook))]
public class PlayerController : MonoBehaviour, IHealth
{
    #region Private Fields
    private const float HEALTH_NORMAL_MAX = 100;
    private const float HEALTH_TOTAL_MAX = 200;
    private bool _isDead = false;

    private float _currentHealth = 80;
    private float _stunTimer = 0;
    private float _stunDuration = 0.5f;

    private PlayerPhysics _physics;
    private PlayerMouseLook _mouseLook;
    private PlayerWeaponHandler _weaponHandler;

    #endregion
    #region Private Methods
    private void Awake()
    {
        _physics = GetComponent<PlayerPhysics>();
        _mouseLook = GetComponent<PlayerMouseLook>();
        _weaponHandler = GetComponent<PlayerWeaponHandler>();
    }

    private void Start()
    {
        _physics.Initialize();
        _mouseLook.Initialize();
        _weaponHandler.Initialize();
    }

    private void Update()
    {
        if (_isDead) return;

        float dt = Time.deltaTime;
        if (_stunTimer > 0)
            _stunTimer -= dt;
        else
        {
            _physics.SetStunned(false);
        }
        InputHandler.InputVars inputs = InputHandler.Instance.Inputs;
        _mouseLook.OnUpdate(delta: dt, inputs);
        _weaponHandler.OnUpdate(delta: dt);
    }

    private void FixedUpdate()
    {
        float fdt = Time.fixedDeltaTime;
        InputHandler.InputVars inputs = InputHandler.Instance.Inputs;
        _physics.OnFixedUpdate(fixedDelta: fdt, inputs);

        if (_isDead) return;

        _weaponHandler.OnFixedUpdate(fixedDelta: fdt, inputs);
    }

    public bool CanHeal()
    {
        return _currentHealth < HEALTH_NORMAL_MAX;
    }
    public bool CanOverHeal()
    {
        return _currentHealth < HEALTH_TOTAL_MAX;
    }
    public void ReduceHealth(float amount)
    {
        if (_isDead || _stunTimer > 0) return;

        _currentHealth -= amount;
        _physics.SetStunned(true);
        _stunTimer = _stunDuration;
        if (_currentHealth <= 0)
        {
            _isDead = true;
            ServiceLocator.Instance.Get<EventHandler>().RaiseOnPlayerDeath();
        }
        else
        {
            ServiceLocator.Instance.Get<EventHandler>().RaiseOnPlayerDamageTaken((int)_currentHealth);
        }
    }

    public void IncreaseHealth(float amount, bool canOverHeal)
    {
        if (_isDead) return;

        float newAmount = _currentHealth + amount;

        if (canOverHeal && newAmount > HEALTH_TOTAL_MAX)
        {
            newAmount = HEALTH_TOTAL_MAX;
        }
        else if (!canOverHeal && newAmount > HEALTH_NORMAL_MAX)
        {
            newAmount = HEALTH_NORMAL_MAX;
        }

        _currentHealth = newAmount;
        ServiceLocator.Instance.Get<EventHandler>().RaiseOnPlayerHeal((int)_currentHealth);
    }
    #endregion
}
