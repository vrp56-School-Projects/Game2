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

    enum OptionType
    {
        PLAYER_OPTIONS,
        BOARD_OPTIONS
    }

    [SerializeField]
    private OptionType _type = OptionType.PLAYER_OPTIONS;

    private TMPro.TextMeshPro _textMesh;

    private PlayerOptionsController _playerOptionsControllerScript;
    private BoardOptionsController _boardOptionsControllerScript;

    [SerializeField]
    private AudioSource _audioSource;

    public GameObject SelectedOption;

    // Start is called before the first frame update
    void Start()
    {
        _playerOptionsControllerScript = this.GetComponentInParent<PlayerOptionsController>();
        _boardOptionsControllerScript = this.GetComponentInParent<BoardOptionsController>();

        _textMesh = SelectedOption.GetComponent<TMPro.TextMeshPro>();
        _textMesh.text = _optionNames[_currentIndex];
    }

    public void CycleRight()
    {
        //Cycle right and handle wrap-around
        _currentIndex = (_currentIndex + 1) % _optionNames.Length;
        _textMesh.text = _optionNames[_currentIndex];

        _audioSource.PlayOneShot(_audioSource.clip);

        switch(_type)
        {
            case OptionType.PLAYER_OPTIONS:
                _playerOptionsControllerScript.UpdateOptions();
                break;

            case OptionType.BOARD_OPTIONS:
                _boardOptionsControllerScript.UpdateBoard();
                break;
        }        
    }

    public void CycleLeft()
    {
        //Cycle left and handle wrap-around
        _currentIndex = (_currentIndex + _optionNames.Length - 1) % _optionNames.Length;
        _textMesh.text = _optionNames[_currentIndex];

        _audioSource.PlayOneShot(_audioSource.clip);

        switch (_type)
        {
            case OptionType.PLAYER_OPTIONS:
                _playerOptionsControllerScript.UpdateOptions();
                break;

            case OptionType.BOARD_OPTIONS:
                _boardOptionsControllerScript.UpdateBoard();
                break;
        }
    }

    public int GetCurrentIndex()
    {
        return _currentIndex;
    }
}
