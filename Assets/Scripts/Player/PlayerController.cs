using Observer;
using UnityEngine;
[RequireComponent(typeof(PlayerInput), typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerWeaponHandler), typeof(PlayerMouseLook))]
public class PlayerController : MonoBehaviour, IHealth
{
    #region Private Fields
    private const float HEALTH_NORMAL_MAX = 100;
    private const float HEALTH_TOTAL_MAX = 150;
    private bool _isDead = false;

    private float _currentHealth = 100;
    private float _stunTimer = 0;
    private float _stunDuration = 1f;

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
        //Debug.Log("Enemy died!");
    }

    private void Start()
    {
        _input.Initialize();
        _physics.Initialize(_input);
        _mouseLook.Initialize(_input);
        _weaponHandler.Initialize(_input);
    }

    private void Update()
    {
        if (_isDead)
            return;

        float dt = Time.deltaTime;
        if (_stunTimer > 0)
            _stunTimer -= dt;
        else
        {
            _physics.SetStunned(false);
        }

        _input.OnUpdate();
        _mouseLook.OnUpdate(delta: dt);
        //_weaponHandler.OnUpdate(delta: dt);
    }

    private void FixedUpdate()
    {
        float fdt = Time.fixedDeltaTime;
        _physics.OnFixedUpdate(fixedDelta: fdt);
        if (_isDead)
            return;

        _weaponHandler.OnFixedUpdate(fixedDelta: fdt);
        //if (_weaponHandler.dirtyFlag)
        //{
            
        //}
    }

    public void ReduceHealth(float amount)
    {
        if (_isDead)
            return;

        if (_stunTimer > 0)
            return;

        _currentHealth -= amount;
        _physics.SetStunned(true);
        _stunTimer = _stunDuration;
        if (_currentHealth <= 0)
        {
            _isDead = true;
            ServiceLocator.Current.Get<EventHandler>().RaiseOnPlayerDeath();
        }
        else
        {
            ServiceLocator.Current.Get<EventHandler>().RaiseOnPlayerDamageTaken();
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (_isDead)
            return;
        float newAmount = _currentHealth + amount;
        if (newAmount > HEALTH_NORMAL_MAX)
        {
            newAmount = HEALTH_NORMAL_MAX;
        }


        _currentHealth = newAmount;
    }

    public void OverHeal(float amount)
    {
        if (_isDead)
        {
            return;
        }
        float newAmount = _currentHealth + amount;
        if (newAmount > HEALTH_TOTAL_MAX)
            newAmount = HEALTH_TOTAL_MAX;
    }
    #endregion
}
