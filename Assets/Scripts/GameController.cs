using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class GameController : MonoBehaviour
{
    public Camera FirstPersonCamera;

    private Anchor anchor;
    private DetectedPlane detectedPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSelectedPlane(DetectedPlane detectedPlane)
    {
        this.detectedPlane = detectedPlane;
        CreateAnchor();
    }

    void CreateAnchor()
    {
        // create an anchor position by raycasting a point to the center of the screen
        Vector2 position = new Vector2(Screen.width * .5f, Screen.height * .5f);
        Ray ray = FirstPersonCamera.ScreenPointToRay(position);
        Vector3 anchorPosition = detectedPlane.CenterPose.position;

        // create an anchor at that point
        if (anchor != null)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            DestroyObject(anchor);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        anchor = detectedPlane.CreateAnchor(new Pose(anchorPosition, Quaternion.identity));

        // attach the object to the anchor
        transform.position = anchorPosition;
        transform.SetParent(anchor.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // tracking state must be FrameTrackingState.Tracking to access Frame
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // if there is no plane, return
        if (detectedPlane == null)
        {
            return;
        }

        
    }
}
