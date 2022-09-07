using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class AR_Cursor_2 : MonoBehaviour
{
    ARSessionOrigin arSessionOrigin;
    Pose placementPose;


    void Start()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        
    }
}
