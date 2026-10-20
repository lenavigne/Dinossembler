using System;
using UnityEngine;

public class PlaceObjectToTargetPlace : MonoBehaviour
{
    Camera cam;
    LayerMask layerObject;
    LayerMask layerTarget;
    LayerMask layerObstacle;

    Transform selectedObject;
    Transform selectedLocation;
    [SerializeField]
    float pickDistance = 4f;
    bool carringObject = false;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        layerObject = LayerMask.GetMask("Bone");
        layerTarget = LayerMask.GetMask("Target");
        layerObstacle = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        if (!carringObject && Input.GetMouseButtonDown(0))
        {
            if (!Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), Mathf.Infinity, layerObstacle))
                SelectObject();
        }
        else if (carringObject && Input.GetMouseButtonUp(0))
        {
            if (!Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), Mathf.Infinity, layerObstacle))
                SelectLocationTarget();
            else
            {
                selectedObject.GetComponent<PickableBehavior>().Released();
                carringObject = false;
            }
        }
    }

    void SelectObject()
    {
        RaycastHit hit;
        // Does the ray intersect object
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerObject))
        {
            if(IsPickable(hit.transform))
            { 
                selectedObject = hit.transform;
                selectedObject.GetComponent<PickableBehavior>().Grabbed(cam.transform);
                carringObject = true;
            }
        }
    }

    void SelectLocationTarget()
    {
        RaycastHit hit;
        // Does the ray intersect target
        Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerTarget);
        selectedLocation = hit.transform;
        selectedObject.GetComponent<PickableBehavior>().Released(selectedLocation);
        carringObject = false;
    }

    bool IsPickable(Transform obj)
    {
        return (Vector3.Distance(gameObject.transform.position, obj.position) <= pickDistance);
    }
}
