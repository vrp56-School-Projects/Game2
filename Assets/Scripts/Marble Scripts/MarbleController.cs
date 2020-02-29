using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
	
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip _wallSound, _marbleSound, _marbleSoundBonk;

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


    public bool buttonPressed = false;
    private bool _activateBarrier = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //Gain access to the scene controller
        GameObject[] objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in objects)
        {
            SceneController ctrl = obj.GetComponentInChildren<SceneController>();

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
        //Only active when marble has been shot AND barrier is up
        if (_currentState == State.INACTIVE && _activateBarrier)
        {
            if (transform.localPosition.z < -2.8f)
            {
                _rigidBody.Sleep();
            }
        }

        if (_rigidBody.IsSleeping() && _currentState == State.INACTIVE && !_isDead)
        {
            _isDead = true;
            _sceneController.EndTurn();
        }
    }

    //Manage player placement of the marble along the bottom edge of the board
    private void ManagePlacementState()
    {
        //location selected
        if (buttonPressed)
        {
            buttonPressed = false;
            _currentState = State.DIRECTION;
            _directionMarker = Instantiate(_directionMarker, this.transform.position, Quaternion.identity);
        }
        // Moving the Marble with the camera
        else
        {
            float x = Camera.main.transform.position.x;
            float z = Camera.main.transform.position.z;

            this.transform.position = new Vector3(x, this.transform.position.y, z);
            Vector3 marblePos = this.transform.localPosition;

            float localX = marblePos.x;

            //Check bounds
            if (localX < -_placementLimit)
            {
                localX = -_placementLimit;
            }
            else if (localX > _placementLimit)
            {
                localX = _placementLimit;
            }

            marblePos.x = localX;
            marblePos.z = -2.95f;
            this.transform.localPosition = marblePos;
        }
    }
	
	// when marble hits something, make noise
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 0 && (collision.gameObject.tag == "marble") && (!_isDead)){
			//plays noise based on how hard it hit something
			double floaty = collision.relativeVelocity.magnitude / _maxVelocity;
			float floaty1 = (float)floaty;
			if (Random.Range(0,10) == 1){
				floaty1 = floaty1 * 0.1f;
				audioSource.PlayOneShot(_marbleSoundBonk, floaty1);
			}
			else{
				audioSource.PlayOneShot(_marbleSound, floaty1);
			}
			
			audioSource.PlayOneShot(_marbleSound, floaty1);
		}
		else if (collision.relativeVelocity.magnitude > 0 && (collision.gameObject.tag == "SideBoard")){
			double floaty = collision.relativeVelocity.magnitude * 0.25;
			float floaty1 = (float)floaty;
			audioSource.PlayOneShot(_wallSound, floaty1);
		}
    }

    //Manage player selection of marble shooting direction
    private void ManageDirectionState()
    {
        //Direction selected
        if (buttonPressed)
        {
            buttonPressed = false;
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
        if (buttonPressed)
        {
            buttonPressed = false;
            PowerMarkerController controller = _powerMarker.GetComponent<PowerMarkerController>();
            controller.LockPower();

            Rigidbody body = this.GetComponent<Rigidbody>();
            body.AddForce(_direction * _maxVelocity * controller.GetPower(), ForceMode.VelocityChange);

            Destroy(_directionMarker);
            Destroy(_powerMarker);

            _currentState = State.INACTIVE;
            StartCoroutine(ActivateBarrierTimer());
        }        
    }

    IEnumerator ActivateBarrierTimer()
    {
        //After 0.1 seconds, activate barrier to prevent 
        //marbles from falling off the back of the board
        yield return new WaitForSeconds(0.1f);
        _activateBarrier = true;
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
