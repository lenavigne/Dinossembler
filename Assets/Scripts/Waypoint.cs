using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] private Waypoint[] next;
    
    public Waypoint NextWaypoint()
    {
        int i = Random.Range(0, next.Length);
        return next[i];
    } 
}
