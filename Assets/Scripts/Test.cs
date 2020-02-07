using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float force = 3.0f;
    public Vector3 direction = new Vector3(0.0f, 0.0f, 1.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody body = this.GetComponent<Rigidbody>();
            body.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
