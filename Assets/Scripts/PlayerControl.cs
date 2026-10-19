using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform PivotCenter;
    [SerializeField] private float speed = 10;
    private Vector3 moveDirection;
    private float rotationX = 0;
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float rotationXlimit = 25.0f;
    private Animator _animator;
    private CharacterController CharacterController;

    public float jumpSpeed = 10;
    public int nbCapa = 0;
    bool isRunning = false;
    float grav = 25f; //gravité
    void Start()
    {
        //gestion du curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        CharacterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //recuperation des inputs
        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        Debug.Log(speedZ);
        if (speedX != 0f || speedZ != 0)
            _animator.SetBool("IsRunning", true);
        else
            _animator.SetBool("IsRunning", false);
        float speedY = moveDirection.y;


        isRunning = (Input.GetKey(KeyCode.LeftShift) && nbCapa > 1); //à implémenter -> si cogne dans obstacle fragile.

        //calcul des vitesses
        if (isRunning && CharacterController.isGrounded) {
            speedX = speedX * speed * 2;
            speedZ = speedZ * speed * 2;
            gameObject.tag = "runningPlayer";
        }
        else
        {
            speedX = speedX * speed;
            speedZ = speedZ * speed;
            gameObject.tag = "walkingPlayer";
        }

        //calcul du mouvement
        moveDirection = forward * speedZ + right * speedX;

        if(Input.GetButton("Jump") && CharacterController.isGrounded && nbCapa > 0)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = speedY;
        }

        if(Input.GetButton("Jump") && !CharacterController.isGrounded && nbCapa > 2 && moveDirection.y<1f)
        {
            grav = 1f;
        }

        if (!CharacterController.isGrounded)
        {
            moveDirection.y -= grav * Time.deltaTime;
        }
        if (CharacterController.isGrounded)
        {
            grav = 20f;
        }

        //application du mouvement
        CharacterController.Move(moveDirection * Time.deltaTime);

        //camera
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationXlimit, rotationXlimit);
        //applique rotation haut/bas
        PivotCenter.transform.localRotation = Quaternion.Euler(rotationX, PivotCenter.transform.localEulerAngles.y, PivotCenter.transform.localEulerAngles.z);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);

    }
}
