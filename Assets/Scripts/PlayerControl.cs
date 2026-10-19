using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    //public Camera PlayerCamera;

    public Transform PivotCenter;

    CharacterController CharacterController;

    public float speed = 10;

    Vector3 moveDirection;

    float rotationX = 0;
    public float rotationSpeed = 2.0f;
    public float rotationXlimit = 25.0f;

    void Start()
    {
        //gestion du curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 forward = -transform.TransformDirection(Vector3.forward);
        Vector3 right = -transform.TransformDirection(Vector3.right);

        //recuperation des inputs
        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        //float speedY = moveDirection.y;

        //calcul des vitesses
        speedX = speedX * speed;
        speedZ = speedZ * speed;

        //calcul du mouvement
        moveDirection = forward * speedZ + right * speedX;

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
