using System.Collections;
using Observer;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _ragdoll;
    private Transform _target;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    
    private AudioSource _audioSource;

    private EnemyFSM _stateMachine;

    private int _groundLayerMask = 1;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _groundLayerMask = 1 << gameObject.layer;
        _groundLayerMask = ~_groundLayerMask;
        _stateMachine = new EnemyFSM(new EnemyIdleState(), this);
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

    public void AlignWithSurface()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit rayHit, 5f, _groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            _ragdoll.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
        }
        else
            _ragdoll.transform.rotation = Quaternion.LookRotation(transform.forward, -transform.up);
    }

    public void SetHostile(Transform target, float delay)
    {
        if (_stateMachine.CurrentState is EnemyHostileState)
            return;

        _target = target;
        StartCoroutine(HostileDelay(delay));
    }
    private IEnumerator HostileDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _audioSource.Play();
        transform.LookAt(_target);
        _agent.enabled = true;
        _stateMachine.ChangeState(new EnemyHostileState(), this);
    }

    public void UpdateDestination()
    {
        if (NavMesh.SamplePosition(_target.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            if (_agent.velocity != Vector3.zero)
                _ragdoll.transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized, _ragdoll.transform.up);

            _agent.destination = hit.position;
        }
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
}
