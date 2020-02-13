using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleOptionController : MonoBehaviour
{
    private int _currentIndex = 0;

    //[SerializeField]
    //private GameObject[] _options;

    [SerializeField]
    private string[] _optionNames;

    private TMPro.TextMeshPro _textMesh;

    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        //if (_optionNames.Length != _options.Length) Debug.Log("Error: The number of options and number of option names does not match!");

        foreach(string name in _optionNames)
        {
            Debug.Log(name.ToString());
        }

        _textMesh = test.GetComponent<TMPro.TextMeshPro>();
        _textMesh.text = _optionNames[_currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CycleRight()
    {
        //Cycle right and handle wrap-around
        _currentIndex = (_currentIndex + 1) % _optionNames.Length;
        _textMesh.text = _optionNames[_currentIndex];
    }

    public void CycleLeft()
    {
        //Cycle left and handle wrap-around
        _currentIndex = (_currentIndex + _optionNames.Length - 1) % _optionNames.Length;
        _textMesh.text = _optionNames[_currentIndex];
    }
}
