using UnityEngine;
using UnityEngine.AI;

public class NavDino : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Waypoint _waypoint;
    //private Waypoint _nextWaypoint;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.SetDestination(_waypoint.transform.position);
    }
    private void FixedUpdate()
    {
        FaceTarget();
        if (IsClose() || _agent.isStopped)
        {
            _waypoint = _waypoint.NextWaypoint();
            _agent.SetDestination(_waypoint.transform.position);
        }
    }

    bool IsClose()
    {
        return (_agent.remainingDistance == _agent.stoppingDistance);
    }
    void FaceTarget()
    {
        Vector3 direction = (_agent.steeringTarget - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
