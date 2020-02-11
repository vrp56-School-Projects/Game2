using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    //The possible internal states the marble can be in
    private enum State
    {
        INACTIVE,
        PLACEMENT,
        DIRECTION,
        POWER
    }

    [SerializeField]
    private GameObject[] _hatIds; //0=No Hat, 1=Blue Cube Hat, 2=Red Cube Hat
    [SerializeField]
    private int _hatIdTest = 0; //Testing purposes. Later will add functionality so player can choose a hat themselves.

    [SerializeField]
    private GameObject[] _trailIds; //0=No Trail, 1=Blue Trail, 2=Red Trail
    [SerializeField]
    private int _trailIdTest = 0; //Testing purposes. Later will add functionality so player can choose a trail themselves.

    //Track the current state
    private State _currentState = State.PLACEMENT;

    [SerializeField]
    private GameObject _directionMarker;
    [SerializeField]
    private GameObject _powerMarker;

    [SerializeField]
    private float _placementSpeed = 2.0f;
    [SerializeField]
    private float _placementLimit = 1.7f;

    [SerializeField]
    private float _maxVelocity = 5.0f;
    private Vector3 _direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHat(_hatIdTest);
        InstantiateTrail(_trailIdTest);
    }

    private void Update()
    {
        //Different actions depend on the current state
        switch(_currentState)
        {
            case State.INACTIVE:
                break;

            case State.PLACEMENT:
                ManagePlacementState();
                break;

            case State.DIRECTION:
                ManageDirectionState();
                break;

            case State.POWER:
                ManagePowerState();
                break;
        }
        
    }

    void InstantiateHat(int _hatID)
    {
        if (_hatID < 0 || _hatID >= _hatIds.Length)
        {
            Debug.LogError("MarbleController::InstantiateHat() Hat ID " + _hatID + " doesn't exist.");
        }
        else
        {
            switch (_hatID)
            {
                case 0:
                    break;
                default:
                    Instantiate(_hatIds[_hatID], this.gameObject.transform, true);
                    break;
            }
        }
    }

    void InstantiateTrail(int _trailId)
    {
        if (_trailId < 0 || _trailId >= _trailIds.Length)
        {
            Debug.LogError("MarbleController::InstantiateTrail() Trail ID " + _trailId + " doesn't exist.");
        } else
        {
            switch (_trailId)
            {
                case 0:
                    break;
                default:
                    Instantiate(_trailIds[_trailId], this.gameObject.transform, true);
                    break;
            }
        }
    }

    //Manage player placement of the marble along the bottom edge of the board
    private void ManagePlacementState()
    {
        //location selected
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentState = State.DIRECTION;
            _directionMarker = Instantiate(_directionMarker, this.transform.position, Quaternion.identity);
        }
        //Moving Left
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * _placementSpeed * Time.deltaTime);

            Vector3 pos = this.transform.position;

            //Check bounds
            if (pos.x < -_placementLimit)
            {
                pos.x = -_placementLimit;
                this.transform.position = pos;
            }
        }
        //Moing Right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * _placementSpeed * Time.deltaTime);

            Vector3 pos = this.transform.position;

            //Check bounds
            if (pos.x >_placementLimit)
            {
                pos.x = _placementLimit;
                this.transform.position = pos;
            }
        }
    }

    //Manage player selection of marble shooting direction
    private void ManageDirectionState()
    {
        //if(_directionMarker.gameObject == null) _directionMarker = Instantiate(_directionMarker, this.transform);

        //Direction selected
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DirectionMarkerController controller = _directionMarker.GetComponent<DirectionMarkerController>();
            controller.LockRotation();

            float angle = controller.GetRotation();

            _direction = Quaternion.AngleAxis(angle, Vector3.up) * _direction;
            _direction = _direction.normalized;

            _powerMarker = Instantiate(_powerMarker, this.transform.position, Quaternion.identity);
            Vector3 angles = _powerMarker.transform.eulerAngles;
            angles.y = angle;
            _powerMarker.transform.eulerAngles = angles;

            _currentState = State.POWER;
        }
    }

    //Manage player selection of marble shooting power
    private void ManagePowerState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PowerMarkerController controller = _powerMarker.GetComponent<PowerMarkerController>();
            controller.LockPower();

            Rigidbody body = this.GetComponent<Rigidbody>();
            body.AddForce(_direction * _maxVelocity * controller.GetPower(), ForceMode.VelocityChange);

            _currentState = State.INACTIVE;
            Destroy(_directionMarker);
            Destroy(_powerMarker);
        }

        
    }
}
