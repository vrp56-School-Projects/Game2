using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
        transform.rotation = Quaternion.identity;
    }
}
