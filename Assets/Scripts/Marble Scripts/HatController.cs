using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField]
    private float _hatHeight = 0.05f;
    private float _rotationY;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(4f, 4f, 4f);
    }

    public void SetRot(float rotation)
    {
        _rotationY = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + _hatHeight, transform.parent.position.z);

        if (transform.parent.tag == "demoMarble")
        {
            transform.eulerAngles = new Vector3(-90, transform.parent.eulerAngles.y, 0);
        } else
        {
            transform.eulerAngles = new Vector3(-90, _rotationY, 0);
        }
        
    }
}
