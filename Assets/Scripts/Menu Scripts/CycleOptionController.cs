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

    private PlayerOptionsController _playerOptionsControllerScript;

    [SerializeField]
    private AudioSource _audioSource;

    public GameObject SelectedOption;

    // Start is called before the first frame update
    void Start()
    {
        _playerOptionsControllerScript = this.GetComponentInParent<PlayerOptionsController>();

        _textMesh = SelectedOption.GetComponent<TMPro.TextMeshPro>();
        _textMesh.text = _optionNames[_currentIndex];
    }

    public void CycleRight()
    {
        //Cycle right and handle wrap-around
        _currentIndex = (_currentIndex + 1) % _optionNames.Length;
        _textMesh.text = _optionNames[_currentIndex];

        _audioSource.PlayOneShot(_audioSource.clip);

        _playerOptionsControllerScript.UpdateOptions();
    }

    public void CycleLeft()
    {
        //Cycle left and handle wrap-around
        _currentIndex = (_currentIndex + _optionNames.Length - 1) % _optionNames.Length;
        _textMesh.text = _optionNames[_currentIndex];

        _audioSource.PlayOneShot(_audioSource.clip);

        _playerOptionsControllerScript.UpdateOptions();
    }

    public int GetCurrentIndex()
    {
        return _currentIndex;
    }
}
