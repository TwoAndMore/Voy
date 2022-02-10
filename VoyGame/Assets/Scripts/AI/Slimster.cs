using UnityEngine;
using UnityEngine.AI;

public class Slimster : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _pointCoffin;

    public Animator animator;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pointCoffin = GameObject.Find("Coffin").transform;
    }

    private void FixedUpdate() => 
        GoToMiddle();

    private void GoToMiddle()
    {
        transform.LookAt(_pointCoffin);
        if(!animator.GetBool("isReversed"))
            _navMeshAgent.SetDestination(_pointCoffin.position);
        else
            transform.Translate(Vector3.back * (_navMeshAgent.speed + 0.5f) * Time.fixedDeltaTime);
    }
}
