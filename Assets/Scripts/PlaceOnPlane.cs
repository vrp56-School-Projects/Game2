using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Moves the ARSessionOrigin in such a way that it makes the given content appear to be
/// at a given location acquired via a raycast.
/// </summary>
[RequireComponent(typeof(ARSessionOrigin))]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    private GameObject spawnObject;

    private bool isPlaced = false;
    private Vector2 rayPos;

    private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private ARSessionOrigin m_SessionOrigin;
    private ARPlaneManager m_ARPlaneManager;
    private ARRaycastManager m_RaycastManager;
    private ARPointCloudManager m_ARPointCloudManager;
    private SceneController m_sceneControllerScript;


    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_ARPointCloudManager = GetComponent<ARPointCloudManager>();

        //m_SessionOrigin.transform.localScale = new Vector3(10, 10, 10);
        //Scale();
    }

    public void SetObject(GameObject board)
    {
        spawnObject = board;
    }

    private void StopPlaneDetection()
    {
        m_ARPointCloudManager.enabled = false;
        m_ARPlaneManager.enabled = false;

        foreach (ARPlane plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(m_ARPlaneManager.enabled);
        }

        foreach (ARPointCloud pointCloud in m_ARPointCloudManager.trackables)
        {
            pointCloud.gameObject.SetActive(m_ARPointCloudManager.enabled);
        }
    }

    public void Place()
    {   
        var touch = Input.GetTouch(0);

        if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            
            isPlaced = true;
            

            StopPlaneDetection();
            m_SessionOrigin.GetComponentInChildren<SceneController>().BoardPlacementComplete();
        }
        
    }

    public void Scale()
    {
        m_SessionOrigin.transform.localScale = new Vector3(10, 10, 10);
    }


    // Update is called once per frame
    void Update()
    {
        if (spawnObject == null)
            return;


        rayPos = new Vector2(Screen.width / 2, Screen.height / 2);

        if (m_RaycastManager.Raycast(rayPos, s_Hits, TrackableType.PlaneWithinBounds) && !isPlaced)
        {
            Vector3 pt = new Vector3 (s_Hits[0].pose.position.x, s_Hits[0].pose.position.y, s_Hits[0].pose.position.z);
            
            // Now lerp to the position                                         
            spawnObject.transform.position = Vector3.Lerp(spawnObject.transform.position, pt,
              Time.smoothDeltaTime * 20f);

            // get board to face camera
            spawnObject.transform.LookAt(Camera.main.transform.position, Vector3.up);
            spawnObject.transform.localEulerAngles = new Vector3(0, spawnObject.transform.localEulerAngles.y, 0);
            spawnObject.transform.Rotate(0, 180f, 0, Space.Self);
        }

        Place();
    }


}
