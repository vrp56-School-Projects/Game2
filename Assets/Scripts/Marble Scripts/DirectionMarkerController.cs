using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMarkerController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 2.0f; //Degrees per second
    private int _rotationDirection = 1;

    [SerializeField]
    private float _maxRotation = 60.0f;

    private bool _rotationLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotationLocked) return;

        //Rotate the marker at the desired speed in the desired direction
        //this.transform.Rotate(Vector3.up, _rotationSpeed * (float)(_rotationDirection) * Time.deltaTime);

        //Get the resulting euler angles
        Vector3 angles = this.transform.localEulerAngles;
        //Debug.Log(angles);

        angles.y = angles.y + _rotationSpeed * _rotationDirection;
        Debug.Log(angles);

        //If we rotated beyond our maximum angle then clamp and flip rotation direction
        if (angles.y > _maxRotation && angles.y < 360.0f - _maxRotation - 15.0f)
        {
            _rotationDirection *= -1;
            angles.y = _maxRotation;
            //this.transform.localEulerAngles = angles;
        }
        else if (angles.y < 360.0f - _maxRotation && angles.y > _maxRotation)
        {
            _rotationDirection *= -1;
            angles.y = 360.0f - _maxRotation;
            //this.transform.localEulerAngles = angles;
        }

        this.transform.localEulerAngles = angles;
    }

    public void LockRotation()
    {
        _rotationLocked = true;
    }

    public float GetRotation()
    {
        return this.transform.eulerAngles.y;
    }

    public float GetRotationLocal()
    {
        return this.transform.localEulerAngles.y;
    }
}
