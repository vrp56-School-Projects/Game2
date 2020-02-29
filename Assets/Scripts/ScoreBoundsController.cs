using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScoreBoundsController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _scoringMarble;

    private float _distance;
    [SerializeField]
    private float _actvationDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90, 0, 0); //Lock Rotation

        _distance = Vector3.Distance(_camera.transform.position, transform.position); //Get dist to camera
        if (_distance < _actvationDistance)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        } else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
