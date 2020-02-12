using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField]
    private float _hatHeight = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(4f, 4f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + _hatHeight, transform.parent.position.z);
        transform.eulerAngles = new Vector3(-90, 0, 0);
    }
}
