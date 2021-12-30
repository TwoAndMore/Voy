using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _lookRadius;
    [SerializeField] private float _patrollingRange;
    [SerializeField] private float _chasingTime;

    private EnemyBehavior _state;
    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private Vector3 _destination;
    private bool _walkPointSet;
    private float _distance;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _target = PlayerManager.instance.player.transform;
        _state = EnemyBehavior.Patrolling;
    }

    private void Update()
    {
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
    }
    
    private void Patrolling()
    {
        StopCoroutine(TimeToChasing());
        
        if(!_walkPointSet)
            FindDestination();
        if(_walkPointSet)
            _navMeshAgent.SetDestination(_destination);

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

        /*if (_distance <= _navMeshAgent.stoppingDistance)
            _state = EnemyBehavior.Attacking;*/

        if (_distance >= _lookRadius)
        {
            StartCoroutine(TimeToChasing());
            _walkPointSet = false;
        }
    }

    private void AttackPlayer()
    {
        _navMeshAgent.SetDestination(transform.position);
        FaceTarget();
    }

    private void FindDestination()
    {
        float randomZ = Random.Range(-_patrollingRange, _patrollingRange);
        float randomX = Random.Range(-_patrollingRange, _patrollingRange);
        
        _destination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(_destination, -transform.up, 2f, _groundLayer))
            _walkPointSet = true;
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookROtation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookROtation, Time.deltaTime * 5f);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_destination,0.5f);
    }

    IEnumerator TimeToChasing()
    {
        yield return new WaitForSeconds(_chasingTime);
        _state = EnemyBehavior.Patrolling;
    }
}
