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

    //Track the current state
    private State _currentState = State.PLACEMENT;

    private GameObject _marbleHat;
    private GameObject _marbleTrail;

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

    private Rigidbody _rigidBody;
    private SceneController _sceneController;
    private bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        //Gain access to the scene controller
        GameObject[] objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in objects)
        {
            SceneController ctrl = obj.GetComponent<SceneController>();

            if (ctrl != null)
            {
                _sceneController = ctrl;
                break;
            }
        }
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

    private void FixedUpdate()
    {
        if(_rigidBody.IsSleeping() && _currentState == State.INACTIVE && !_isDead)
        {
            _isDead = true;
            _sceneController.EndTurn();
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
        //Moving Right
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

            Destroy(_directionMarker);
            Destroy(_powerMarker);

            _currentState = State.INACTIVE;
        }        
    }

    public void SetHat(GameObject hat)
    {
        Destroy(_marbleHat);
        _marbleHat = Instantiate(hat, this.gameObject.transform, true);
    }

    public void SetTrail(GameObject trail)
    {
        Destroy(_marbleTrail);
        _marbleTrail = Instantiate(trail, this.gameObject.transform, true);
    }
}
