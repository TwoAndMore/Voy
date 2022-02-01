using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Skeletron : MonoBehaviour
{
    private const float AWAKETIME = 5f;
    private const float TRIGGEREDTIME = 5f;
    private const float AVOIDSTUCKTIME = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip _patrollingSound;
    [SerializeField] private AudioClip _attackSound;
    
    [Header("Variables")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _lookRadius;
    [SerializeField] private float _patrollingRange;
    [SerializeField] private float _chasingTime;

    private EnemyBehavior _state;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private AudioSource _audioSource;
    private AudioSource _chaseSoundSource;
    private AudioSource _attackSource;
    private Transform _target;
    private Vector3 _destination;
    private bool _walkPointSet;
    private bool _isActive;
    private float _distance;
    private bool _setStuckCoroutine;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = transform.Find("Body").GetComponent<Animator>();
        _chaseSoundSource = transform.Find("ChaseSoundObject").GetComponent<AudioSource>();
        _attackSource = transform.Find("AttackSoundObject").GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(SetActive(AWAKETIME));
        
        _target = PlayerManager.instance.player.transform;
        _state = EnemyBehavior.Patrolling;
    }

    private void Update()
    {
        Debug.Log(_state);
        if (!_isActive)
            return;

        switch (_state)
        {
            case EnemyBehavior.Patrolling:
                Patrolling();
                break;
            case EnemyBehavior.Chasing:
                ChasePlayer();
                break;
            case EnemyBehavior.Attacking:
                AttackPlayer();
                break;
        }
        
        _distance = Vector3.Distance(_target.position, transform.position);

        _chaseSoundSource.enabled = _state == EnemyBehavior.Chasing;
        _attackSource.enabled = _state == EnemyBehavior.Attacking;
        
        ChangeAnimationByState();
    }
    
    
    private void OnTriggerEnter(Collider other) => 
        StartCoroutine(SetActive(TRIGGEREDTIME));

    private void Patrolling()
    {
        StopCoroutine(TimeToChasing(0f));
        
        if(!_walkPointSet)
            FindDestination();
        if (_walkPointSet)
        {
            _navMeshAgent.SetDestination(_destination);

            if (!_setStuckCoroutine)
            {
                _setStuckCoroutine = true;
                StartCoroutine(AvoidStuck());
            }
        }

        _navMeshAgent.speed = 2;

        Vector3 distanceToDestination = transform.position - _destination;
        if (distanceToDestination.magnitude < _navMeshAgent.stoppingDistance) 
            _walkPointSet = false;
        
        if (_distance <= _lookRadius)
            _state = EnemyBehavior.Chasing;
    }

    private void ChasePlayer()
    {
        FaceTarget();
        _navMeshAgent.SetDestination(_target.position);

        if (_distance <= _navMeshAgent.stoppingDistance)
            _state = EnemyBehavior.Attacking;

        _navMeshAgent.speed = 3;
        
        if (_distance >= _lookRadius)
        {
            StartCoroutine(TimeToChasing(_chasingTime));
            _walkPointSet = false;
        }
    }

    private void AttackPlayer()
    {
        FaceTarget();
        DoDamage();
        _state = EnemyBehavior.Chasing;
    }

    private void FindDestination()
    {
        float randomZ = Random.Range(-_patrollingRange, _patrollingRange);
        float randomX = Random.Range(-_patrollingRange, _patrollingRange);

        _destination = new Vector3(transform.position.x + randomX , transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(_destination, -transform.up, 2f, _groundLayer))
            _walkPointSet = true;
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookROtation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookROtation, Time.deltaTime * 5f);
    }

    private void ChangeAnimationByState()
    {
        if (_state == EnemyBehavior.Patrolling)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsAttacking", false);
        }
        else if (_state == EnemyBehavior.Chasing)
        {
            _animator.SetBool("IsRunning", true);
        }
        else if(_state == EnemyBehavior.Attacking)
        {
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsAttacking", true);
        }
    }

    private void DoDamage()
    {
        Debug.Log("Did");
        //lok at me
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_destination,0.5f);
    }

    private IEnumerator TimeToChasing(float _time)
    {
        yield return new WaitForSeconds(_time);
        _state = EnemyBehavior.Patrolling;
    }

    private IEnumerator SetActive(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        _isActive = true;
        _audioSource.clip = _patrollingSound;
    }

    private IEnumerator AvoidStuck()
    {
        yield return new WaitForSeconds(AVOIDSTUCKTIME);
        FindDestination();
        _setStuckCoroutine = false;
    }
}
