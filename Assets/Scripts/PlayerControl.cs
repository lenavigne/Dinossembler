using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform PivotCenter;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private float speed = 10;
    private Vector3 moveDirection;
    private float rotationX = 0;
    [SerializeField] private float rotationSpeed = 2.0f;
    private Animator _animator;
    private CharacterController CharacterController;
    private AudioSource _audioSource;
    public float loopSongSpeed = 0.6f;

    public float jumpSpeed = 10;
    bool isRunning = false;
    float grav = 25f; //gravité
    void Start()
    {
        //gestion du curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        CharacterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //recuperation des inputs
        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        //Debug.Log(speedZ);
        if (speedX != 0f || speedZ != 0)
        {
            _animator.SetBool("IsRunning", true);
            _audioSource.mute = false;
        }
        else
        {
            _animator.SetBool("IsRunning", false);
            _audioSource.mute = true;
        }
        float speedY = moveDirection.y;


        isRunning = (Input.GetKey(KeyCode.LeftShift) && sceneController.nb_skel_complete > 1); 

        //calcul des vitesses
        if (isRunning && CharacterController.isGrounded) {
            speedX = speedX * speed * 2;
            speedZ = speedZ * speed * 2;
            _audioSource.pitch = loopSongSpeed * 2;
            gameObject.tag = "runningPlayer";
        }
        else
        {
            speedX = speedX * speed;
            speedZ = speedZ * speed;
            _audioSource.pitch = loopSongSpeed;
            gameObject.tag = "walkingPlayer";
        }

        //calcul du mouvement
        moveDirection = forward * speedZ + right * speedX;

        if(Input.GetButton("Jump") && CharacterController.isGrounded && sceneController.nb_skel_complete > 0)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = speedY;
        }

        if(Input.GetButton("Jump") && !CharacterController.isGrounded && sceneController.nb_skel_complete > 2 && moveDirection.y<1f)
        {
            grav = 2f;
        }

        if (!CharacterController.isGrounded)
        {
            moveDirection.y -= grav * Time.deltaTime;
        }

        if (CharacterController.isGrounded) //|| (Input.GetButton("Jump") && isFlying)
        {
            grav = 20f;
        }

        //application du mouvement
        CharacterController.Move(moveDirection * Time.deltaTime);

        //camera
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -80, 60);
        //applique rotation haut/bas
        PivotCenter.transform.localRotation = Quaternion.Euler(rotationX, PivotCenter.transform.localEulerAngles.y, PivotCenter.transform.localEulerAngles.z);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);

    }

    private IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Breakable") && isRunning)
        {
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitWhile(() => audioSource.isPlaying);
            Destroy(collision.gameObject);
        }
    }
}
