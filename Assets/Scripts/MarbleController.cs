using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    //The possible internal states the marble can be in
    private enum State
    {
        INACTIVE,
        DIRECTION,
        POWER
    }

    //Track the current state
    private State _currentState = State.DIRECTION;

    [SerializeField]
    private GameObject _directionMarker;

    [SerializeField]
    private float _maxVelocity = 3.0f;
    public Vector3 direction = new Vector3(0.0f, 0.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        _directionMarker = Instantiate(_directionMarker, this.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        //Different actions depend on the current state
        switch(_currentState)
        {
            case State.INACTIVE:
                break;

            case State.DIRECTION:
                ManageDirectionState();
                break;

            case State.POWER:
                ManagePowerState();
                break;
        }
        
    }

    private void ManageDirectionState()
    {
        //if(_directionMarker.gameObject == null) _directionMarker = Instantiate(_directionMarker, this.transform);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _directionMarker.GetComponent<DirectionMarkerController>().LockRotation();
            _currentState = State.POWER;
        }
    }

    private void ManagePowerState()
    {
        Rigidbody body = this.GetComponent<Rigidbody>();
        body.AddForce(direction * _maxVelocity, ForceMode.VelocityChange);

        _currentState = State.INACTIVE;
        Destroy(_directionMarker);
        _directionMarker = null;
    }
}
