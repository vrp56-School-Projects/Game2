using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDirectionController : MonoBehaviour
{
    [SerializeField]
    private Camera _firstPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        _firstPersonCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(_firstPersonCamera.transform);
        this.transform.Rotate(Vector3.up, 180.0f,Space.Self);
    }
}
