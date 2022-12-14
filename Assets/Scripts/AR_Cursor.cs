using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR_Cursor : MonoBehaviour
{
    [SerializeField] GameObject cursorChildObject;
    [SerializeField] GameObject objectToPlace;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARSessionOrigin aRSessionOrigin;

    [SerializeField] bool isUsingCursor = true;

    void Start()
    {
        cursorChildObject.SetActive(isUsingCursor);
    }


    void Update()
    {
        if (isUsingCursor)
        {
            UpdateCursor();
        }

        CheckInput();
    }


    void CheckInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (isUsingCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
    }


    void UpdateCursor()
    {
        Vector2 screenPos = aRSessionOrigin.camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        //Vector2 screenPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
