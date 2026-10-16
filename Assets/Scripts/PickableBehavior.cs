using UnityEngine;

public class PickableBehavior : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float pickDistance = 4f;
    private bool hasPlayer = false;
    private bool beingCarried = false;
    private bool touched = false;

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);

        hasPlayer = (dist <= pickDistance);

        //si clic gauche appuyé
        if(hasPlayer && Input.GetMouseButtonDown(0)) //Input.GetMouseButtonDown(0)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }

        if (beingCarried)
        {
            //si objet rentre en collision avec qqch.
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            //sinon si objet est lache.
            else if (Input.GetMouseButtonUp(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }

    private void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}
