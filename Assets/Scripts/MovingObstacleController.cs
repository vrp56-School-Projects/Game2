using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleController : MonoBehaviour
{

    [SerializeField]
    private GameObject _pointA, _pointB;

    private bool _switchDirection = false;
    [SerializeField]
    private float _speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == _pointA.transform.position)
        {
            _switchDirection = true;
        } else if (transform.position == _pointB.transform.position)
        {
            _switchDirection = false;
        }

        if (_switchDirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.transform.position, _speed * Time.deltaTime);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.transform.position, _speed * Time.deltaTime);
        }
    }
}
