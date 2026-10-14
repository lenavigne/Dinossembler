using System;
using UnityEngine;

public class PlaceObjectToTargetPlace : MonoBehaviour
{
    Camera cam;
    LayerMask layerObject;
    LayerMask layerTarget;

    public GameObject selectedObject;
    Transform selectedLocation;
    [SerializeField]
    float speed = 1.0f;
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        layerObject = LayerMask.GetMask("Bones");
        layerTarget = LayerMask.GetMask("Target");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SelectLocationTarget();
        }
        if (selectedLocation != null)
        {
            selectedObject.GetComponent<Rigidbody>().useGravity = false;
            MoveToTarget();
            if (IsPlaced())
            {
                SaveBonesPlacement();
            }
        }
    }

    void SelectLocationTarget()
    {
        RaycastHit hit;
        // Does the ray intersect target
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerTarget))
        {
            selectedLocation = hit.transform;
        }
    }

    void MoveToTarget()
    {
        selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, selectedLocation.transform.position, speed * Time.deltaTime);
    }

    void SaveBonesPlacement()
    {
        if (IsTheRightBones())
        {
            selectedObject.transform.SetParent(selectedLocation.parent, true);
            Destroy(selectedLocation.gameObject);
        }
        else
        {
            selectedObject.GetComponent<Rigidbody>().useGravity = true;
            selectedLocation = null;
        }
    }

    bool IsPlaced()
    {
        return Vector3.Distance(selectedObject.transform.position, selectedLocation.transform.position) == 0f;
    }

    bool IsTheRightBones()
    {
        return selectedObject.transform.name == selectedLocation.name;
    }
}
