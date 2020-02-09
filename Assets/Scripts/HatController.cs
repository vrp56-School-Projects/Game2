using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField]
    private float _hatHeight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + _hatHeight, transform.parent.position.z);
        transform.rotation = Quaternion.identity;
    }
}
