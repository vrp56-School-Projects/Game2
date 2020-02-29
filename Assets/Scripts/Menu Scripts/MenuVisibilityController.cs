using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVisibilityController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.Hide();
    }

    public void Show()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
    }
}
