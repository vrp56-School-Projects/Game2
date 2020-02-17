using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleOrbitController : MonoBehaviour
{
    [SerializeField]
    private float _radius = 1.15f;

    [SerializeField]
    private float _angularVelocity = 65.0f;

    private float _currentAngle = 0.0f;

    private float _facingAngleOffset = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentAngle = (_currentAngle + _angularVelocity * Time.deltaTime) % 360.0f;

        Vector3 newPos = PolarToCartesion(new Vector2(0, _currentAngle));

        this.transform.localPosition = newPos;

        Vector3 angles = this.transform.eulerAngles;
        angles.y = _currentAngle + _facingAngleOffset;

        this.gameObject.transform.eulerAngles = angles;
    }
    
    private Vector3 PolarToCartesion(Vector2 polar)
    {
        Vector3 origin = new Vector3(0, -0.45f, _radius);

        Quaternion rotation = Quaternion.Euler(polar.x, polar.y, 0);

        return rotation * origin;
    }
}