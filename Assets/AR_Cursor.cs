using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR_Cursor : MonoBehaviour
{
    [SerializeField] GameObject cursorChildObject;
    [SerializeField] GameObject objectToPlace;
    [SerializeField] ARRaycastManager raycastManager;

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
    }

    void UpdateCursor()
    {
        Vector2 screenPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
