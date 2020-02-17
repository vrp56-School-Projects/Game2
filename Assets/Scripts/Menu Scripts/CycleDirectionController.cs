using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleDirectionController : MonoBehaviour
{
    [SerializeField]
    private int _direction = 1;

    private CycleOptionController _cycleOptionScript;

    // Start is called before the first frame update
    void Start()
    {
        _cycleOptionScript = this.GetComponentInParent<CycleOptionController>();
    }

    private void OnMouseDown()
    {
        //The Direction arrow has been clicked. Cycle the selected option in the configured direction

        if (_direction == 1) _cycleOptionScript.CycleRight();

        else _cycleOptionScript.CycleLeft();
    }
}
