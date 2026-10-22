using System;
using UnityEngine;

public class PlaceObjectToTargetPlace : MonoBehaviour
{
    private Camera cam;
    private LayerMask layerObject;
    private LayerMask layerTarget;
    private LayerMask layerObstacle;
    
    private Transform selectedObject;
    private Transform selectedLocation;
    [SerializeField] private float pickDistance = 5f;
    [SerializeField] private float selectDistance = 20f;
    private bool carringObject = false;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        layerObject = LayerMask.GetMask("Bone");
        layerTarget = LayerMask.GetMask("Target");
        layerObstacle = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        //RaycastHit h;
        //RaycastHit hw;
        //if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out h, Mathf.Infinity, layerTarget))
        //{
        //    if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hw, Mathf.Infinity, layerObstacle))
        //    {
        //        if(hw.distance < h.distance)//target derrière mur
        //        {
        //            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hw.distance, Color.blue);
        //        }
        //        else
        //        {
        //            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * h.distance, Color.red);
        //        }
        //    }
        //    else
        //    {
        //        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hw.distance, Color.blue);
        //    }
            
        //    //Debug.Log(h.transform.name);
        //}
        //else
        //    Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 100, Color.white);

        if (!carringObject && Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }
        else if (carringObject && Input.GetMouseButtonUp(0))
        {
            carringObject = false;
            SelectLocationTarget();
        }
    }

    void SelectObject()
    {
        RaycastHit hit;
        // Does the ray intersect object
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerObject))
        {
            if(hit.distance <= pickDistance)
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
        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerTarget))
        {
            Debug.Log(hit.distance);
            if(hit.distance < selectDistance)
            {
                RaycastHit hitObstacle;
                if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitObstacle, Mathf.Infinity, layerObstacle))
                {
                    selectedLocation = (hitObstacle.distance < hit.distance) ? null : hit.transform;
                        //si mur devant target
                }
                //Debug.Log("target selected : " + selectedLocation.name);
            }
            else
                selectedLocation = null;
        }
        else
            selectedLocation = null;

        selectedObject.GetComponent<PickableBehavior>().Released(selectedLocation);
    }

    bool IsPickable(Transform obj)
    {
        return (Vector3.Distance(gameObject.transform.position, obj.position) <= pickDistance);
    }
}
