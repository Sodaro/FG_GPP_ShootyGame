using Observer;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject _ragdoll;
    [SerializeField] private float _hostileSpeed;
    [SerializeField] private float _idleeSpeed;
    #endregion

    #region Private Fields
    private Transform _target;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private EnemyFSM _stateMachine;
    private Vector3 _startPosition;
    private int _groundLayerMask = 1;
    #endregion

    #region Public Methods
    public bool HasTarget => _target != null;

    public bool CheckTargetPathValididty()
    {
        if (NavMesh.SamplePosition(_target.position , out NavMeshHit hit, 2f, NavMesh.AllAreas))
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

    public float DistanceToTarget
    {
        get
        {
            if (_target == null)
                return 0;
            else
            {
                return Vector3.Distance(transform.position, _target.position);
            }
        }
    }

    public void AlignWithSurface()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit rayHit, 5f, _groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            _ragdoll.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
        }
        else
            _ragdoll.transform.rotation = Quaternion.LookRotation(transform.forward, -transform.up);
    }

    public void AssignTarget(Transform target)
    {
        if (_stateMachine.CurrentState is EnemyHostileState)
            return;

        _target = target;
    }

    public void SetHostile()
    {
        _audioSource.Play();
        transform.LookAt(_target);
        _agent.enabled = true;
        _stateMachine.ChangeState(new EnemyHostileState(), this);
    }

    public void ReturnToStart()
    {
        _agent.destination = _startPosition;
        _stateMachine.ChangeState(new EnemyIdleState(), this);
    }

    public void UpdateDestination()
    {
        //return whether destination is reachable
        if (_agent.velocity != Vector3.zero)
            _ragdoll.transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized, _ragdoll.transform.up);

        _agent.destination = _target.position;
    }

    public void Kill(Vector3 forceDir)
    {
        _agent.enabled = false;
        _rigidbody.transform.parent = null;

        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(forceDir, ForceMode.Impulse);
        _rigidbody.AddTorque(forceDir, ForceMode.Impulse);
        ServiceLocator.Current.Get<EventHandler>().RaiseOnEnemyDeath();
        Destroy(gameObject);
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
        _stateMachine.FixedUpdate();
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
