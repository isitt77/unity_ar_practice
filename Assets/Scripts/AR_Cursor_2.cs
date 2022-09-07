using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class AR_Cursor_2 : MonoBehaviour
{
    GameObject arCursor; 
    ARSessionOrigin arSessionOrigin;
    ARRaycastManager raycastManager;
    Pose placementPose;
    bool placementPoseIsValid = false;


    void Start()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
        arCursor = FindObjectOfType<GameObject>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }


    void UpdatePlacementPose()
    {
        Vector2 screenCenter = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
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
