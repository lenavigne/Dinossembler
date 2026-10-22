using UnityEngine;

public class PickableBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _col;
    private bool _isGrabbed = false;
    private Transform _target;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _angSpeed = 2.0f;

    void Start() 
    {
        _rb = GetComponent<Rigidbody>(); 
        _col = GetComponent<Collider>();
    }

    void Update()
    {
        if (!_isGrabbed)
        {
            if (_target != null)
            {
                //Debug.Log("moving");
                MoveToTarget();
                if (IsPlaced())
                    CheckingBone();
            }
        }
    }
    public void Grabbed(Transform player)
    {
        _rb.isKinematic = true;
        _col.isTrigger = true;
        transform.SetParent(player, true);
        _isGrabbed = true;
    }

    public void Released(Transform t = null)
    {
        _isGrabbed = false;
        _col.isTrigger = false;
        _rb.isKinematic = (t != null);
        _rb.useGravity = (t == null);
        transform.parent = null;
        _target = t;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            _target.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            _target.rotation, _angSpeed * Time.deltaTime);
    }

    bool IsPlaced()
    {
        return Vector3.Distance(transform.position, _target.position) == 0 && Vector3.Angle(transform.eulerAngles, _target.eulerAngles) <= 1f;
    }

    void CheckingBone()
    {
        //Debug.Log("_target Name : " + _target.name);
        //Debug.Log("Bone Name : " + transform.name);
        if (transform.name == _target.name)
        {
            transform.SetParent(_target.parent, true);
            _rb.isKinematic = true;
            transform.gameObject.layer = 9;
            Destroy(_target.gameObject);
            transform.GetComponentInParent<CompleteSkeleton>().BonePlaced();
        }
        else
        {
            GetComponent<AudioSource>().Play();
            _rb.useGravity = true;
            _rb.isKinematic = false;
        }
        _target = null;
    }


}
