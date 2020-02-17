using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Material[] _marbleTextures;

    [SerializeField]
    private GameObject[] _marbleTrails;

    [SerializeField]
    private GameObject[] _marbleHats;

    [SerializeField]
    private GameObject _playerMarblePrefab;

    [SerializeField]
    private GameObject _scoreMarblePrefab;

    [SerializeField]
    private Vector3 _marbleStartPosition;

    //Player 1 Information
    private List<GameObject> _player1Marbles;
    private int _player1TextureIndex;
    private int _player1TrailIndex;
    private int _player1HatIndex;

    //Player 2 Information
    private List<GameObject> _player2Marbles;
    private int _player2TextureIndex;
    private int _player2TrailIndex;
    private int _player2HatIndex;

    private GameObject _demoMarble;

    [SerializeField]
    private GameObject _playerOptionsMenu;

    //The Possible Game States
    private enum GameState
    {
        BOARD_PLACEMENT,
        BOARD_SELECTION,
        PLAYER1_OPTIONS,
        PLAYER2_OPTIONS,
        PLAYER1_TURN,
        PLAYER2_TURN,
        END_GAME
    }

    //Track the current game state
    private GameState _currentState = GameState.PLAYER1_OPTIONS;

    // Start is called before the first frame update
    void Start()
    {
        _player1Marbles = new List<GameObject>();
        _player2Marbles = new List<GameObject>();

        //Create and show the menu with the orbiting marble
        _demoMarble = Instantiate(_playerMarblePrefab, new Vector3(_playerOptionsMenu.transform.position.x, -0.55f, _playerOptionsMenu.transform.position.z), Quaternion.identity, _playerOptionsMenu.transform);
        _demoMarble.AddComponent<MarbleOrbitController>();
        _playerOptionsMenu.GetComponent<MenuVisibilityController>().Show();

        SetMarbleTexture(0);
        SetMarbleTrail(0);
        SetMarbleHat(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMarbleTexture(int index)
    {
        _demoMarble.GetComponent<Renderer>().material = _marbleTextures[index];
    }

    public void SetMarbleTrail(int index)
    {
        _demoMarble.GetComponent<MarbleController>().SetTrail(_marbleTrails[index]);
    }

    public void SetMarbleHat(int index)
    {
        _demoMarble.GetComponent<MarbleController>().SetHat(_marbleHats[index]);
    }

    public void MenuClosed()
    {
        switch(_currentState)
        {
            case GameState.BOARD_SELECTION:
                _currentState = GameState.PLAYER1_OPTIONS;
                break;

            case GameState.PLAYER1_OPTIONS:
                break;

            case GameState.PLAYER2_OPTIONS:
                break;
        }
        _playerOptionsMenu.GetComponent<MenuVisibilityController>().Hide();
    }

    public void EndTurn()
    {
        switch(_currentState)
        {
            case GameState.PLAYER1_TURN:
                _currentState = GameState.PLAYER2_TURN;
                break;

            case GameState.PLAYER2_TURN:
                _currentState = GameState.PLAYER1_TURN;
                break;
        }

        if (_player2Marbles.Count >= 5) _currentState = GameState.END_GAME;
    }
}
