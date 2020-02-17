using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVisibilityController : MonoBehaviour
{
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = this.GetComponent<Renderer>();
        _renderer.enabled = false;
    }

    public void Show()
    {
        _renderer.enabled = true;
    }

    public void Hide()
    {
        _renderer.enabled = false;
    }
}
