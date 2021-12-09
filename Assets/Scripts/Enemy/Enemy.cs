using Observer;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class Enemy : MonoBehaviour, IHealth
{
    #region Serialized Fields
    [SerializeField] private GameObject _ragdoll;
    [SerializeField] private float _hostileSpeed;
    [SerializeField] private float _idleSpeed;
    #endregion

    #region Private Fields
    private float _currentHealth = 1;
    private Transform _target;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private EnemyFSM _stateMachine;
    private Vector3 _startPosition;
    private int _groundLayerMask = 1;
    #endregion

    #region Public Methods

    #region State Methods

    public bool HasTarget => _target != null;

    public float? DistanceToTarget
    {
        get
        {
            if (_target == null)
            {
                return null;
            }  
            else
            {
                return Vector3.Distance(transform.position, _target.position);
            }
        }
    }

    public bool CheckTargetPathValididty()
    {
        if (_target == null)
            return false;

        if (NavMesh.SamplePosition(_target.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(hit.position, path);
            if (path.status == NavMeshPathStatus.PathInvalid)
                return false;
            else
                return true;
        }
        return false;
    }
    public void SetHostile()
    {
        _agent.speed = _hostileSpeed;
        _audioSource.PlaySoundRandomPitch(0.8f, 1.2f);
        transform.LookAt(_target);
        _agent.enabled = true;
        _stateMachine.ChangeState(new EnemyHostileState(), this);
    }

    public void SetIdle()
    {
        _agent.speed = _idleSpeed;
        _agent.destination = _startPosition;
        _stateMachine.ChangeState(new EnemyIdleState(), this);
    }

    public void UpdateDestination()
    {
        _agent.destination = _target.position;
    }
    #endregion

    public void AssignTarget(Transform target)
    {
        if (_stateMachine.CurrentState is EnemyHostileState)
            return;

        _target = target;
    }

    public void ReduceHealth(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            if (_target == null)
            {
                Kill(-transform.forward * amount);
            }
            else
            {
                Kill((transform.position - _target.position).normalized * amount);
            }

            ServiceLocator.Instance.Get<EventHandler>().RaiseOnEnemyDeath();
        }
    }

    public void IncreaseHealth(float amount, bool canOverHeal)
    {
        //the enemies currently have no way of healing, but could be an added feature in the future
        throw new System.NotImplementedException();
    }

    #endregion

    #region Private Methods
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _groundLayerMask = 1 << gameObject.layer;
        _groundLayerMask = ~_groundLayerMask;
        _stateMachine = new EnemyFSM(new EnemyIdleState(), this);
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_target == null)
            return;
        if (!_agent.enabled)
            return;

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        AlignWithSurface();
    }

    private void AlignWithSurface()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit _, 5f, _groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            _ragdoll.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
        }
        else
            _ragdoll.transform.rotation = Quaternion.LookRotation(transform.forward, -transform.up);
    }

    private void Kill(Vector3 forceDir)
    {
        _agent.enabled = false;
        _rigidbody.transform.parent = null;

        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(forceDir, ForceMode.Impulse);
        _rigidbody.AddTorque(forceDir, ForceMode.Impulse);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.TryGetComponent(out PlayerController player))
        {
            player.ReduceHealth(10f);
        }
    }
    #endregion
}
