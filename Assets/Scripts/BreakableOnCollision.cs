using UnityEngine;

public class BreakableOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("runningPlayer"))
        {
            Destroy(gameObject);
        }
    }
}
