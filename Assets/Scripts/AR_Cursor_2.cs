using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class AR_Cursor_2 : MonoBehaviour
{
    [SerializeField] GameObject objectToPlace;
    [SerializeField] GameObject arCursor;
    ARSessionOrigin arSessionOrigin;
    ARRaycastManager raycastManager;
    Pose placementPose;
    bool placementPoseIsValid = false;


    void Start()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // Checking plcament pose, finger touch, and first finger began touch...
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }


    void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }


    void UpdatePlacementPose()
    {
        Vector3 screenCenter = arSessionOrigin.camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            Vector3 cameraForward = arSessionOrigin.camera.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            arCursor.SetActive(true);
            arCursor.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            arCursor.SetActive(false);
        }
    }
}
