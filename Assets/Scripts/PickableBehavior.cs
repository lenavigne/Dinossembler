using UnityEngine;

public class PickableBehavior : MonoBehaviour
{
    Rigidbody _rb;
    bool _isGrabbed = false;
    Transform _target;
    [SerializeField]
    float _speed = 2.0f;

    void Start() 
    {
        _rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        if (!_isGrabbed && _target != null)
        {
            MoveToTarget();
            if (IsPlaced())
                CheckingBone();
        }
    }
    public void Grabbed(Transform player)
    {
        _rb.isKinematic = true;
        transform.SetParent(player, true);
        _isGrabbed = true;
    }

    public void Released(Transform t = null)
    {
        _isGrabbed = false;
        _rb.isKinematic = false;
        transform.parent = null;
        _target = t;
        _rb.useGravity = (t == null);
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            _target.position, _speed * Time.deltaTime);
    }

    bool IsPlaced()
    {
        return Vector3.Distance(transform.position, _target.position) == 0;
    }

    void CheckingBone()
    {
        Debug.Log("_target Name : " + _target.name);
        Debug.Log("Bone Name : " + transform.name);
        if (transform.name == _target.name)
        {
            transform.SetParent(_target.parent, true);
            Destroy(_target.gameObject);
        }
        else
        {
            _rb.useGravity = true;
        }
        _target = null;
    }
    //public Transform player;
    //public Transform playerCam;
    //public float pickDistance = 4f;
    //private bool hasPlayer = false;
    //private bool beingCarried = false;
    //private bool touched = false;

    //void Update()
    //{
    //    //float dist = Vector3.Distance(gameObject.transform.position, player.position);

    //    //hasPlayer = (dist <= pickDistance);

    //    //si clic gauche appuyé
    //    if(IsPickable(player) && Input.GetMouseButtonDown(0)) //Input.GetMouseButtonDown(0)
    //    {
    //        GetComponent<Rigidbody>().isKinematic = true;
    //        transform.parent = playerCam;
    //        beingCarried = true;
    //    }

    //    if (beingCarried)
    //    {
    //        //si objet rentre en collision avec qqch.
    //        if (touched)
    //        {
    //            GetComponent<Rigidbody>().isKinematic = false;
    //            transform.parent = null;
    //            beingCarried = false;
    //            touched = false;
    //        }
    //        //sinon si objet est lache.
    //        else if (Input.GetMouseButtonUp(0))
    //        {
    //            GetComponent<Rigidbody>().isKinematic = false;
    //            transform.parent = null;
    //            beingCarried = false;
    //        }
    //    }
    //}

    //bool IsPickable(Transform player)
    //{
    //    return (Vector3.Distance(gameObject.transform.position, player.position) <= pickDistance);
    //}

    //private void OnTriggerEnter()
    //{
    //    if (beingCarried)
    //    {
    //        touched = true;
    //    }
    //}


}
