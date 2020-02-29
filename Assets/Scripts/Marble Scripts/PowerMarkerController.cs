using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMarkerController : MonoBehaviour
{
    private int _scaleDirection = -1;

    [SerializeField]
    private float _scaleSpeed = 175.0f;

    [SerializeField]
    private float _minScale = 10.0f;
    [SerializeField]
    private float _maxScale = 100.0f;

    private bool _powerLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_powerLocked) return;

        Vector3 currScale = this.transform.localScale;

        float zScale = currScale.z;

        zScale = zScale + _scaleDirection * _scaleSpeed * Time.deltaTime;

        if (zScale > _maxScale)
        {
            zScale = _maxScale;
            _scaleDirection *= -1;
        }
        else if (zScale < _minScale)
        {
            zScale = _minScale;
            _scaleDirection *= -1;
        }

        currScale.z = zScale;
        this.transform.localScale = currScale;
    }

    public void LockPower()
    {
        _powerLocked = true;
    }

    public float GetPower()
    {
        return this.transform.localScale.z / _maxScale;
    }
}