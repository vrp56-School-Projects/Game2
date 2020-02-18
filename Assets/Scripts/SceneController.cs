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
    private GameObject _playerOptionsMenuPrefab;

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
        _playerOptionsMenu = Instantiate(_playerOptionsMenuPrefab);
        _demoMarble = Instantiate(_playerMarblePrefab, new Vector3(_playerOptionsMenu.transform.position.x, -0.55f, _playerOptionsMenu.transform.position.z), Quaternion.identity, _playerOptionsMenu.transform);
        _demoMarble.AddComponent<MarbleOrbitController>();
        SetMarbleTexture(0);
        SetMarbleTrail(0);
        SetMarbleHat(0);
        _playerOptionsMenu.GetComponent<MenuVisibilityController>().Show();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Displays the selected texture on the demo marble and updates the corresponding player's selected texture
    public void SetMarbleTexture(int index)
    {
        switch (_currentState)
        {
            case GameState.PLAYER1_OPTIONS:
                _player1TextureIndex = index;
                break;

            case GameState.PLAYER2_OPTIONS:
                _player2TextureIndex = index;
                break;
        }
        _demoMarble.GetComponent<Renderer>().material = _marbleTextures[index];
    }

    //Displays the selected trail on the demo marble and updates the corresponding player's selected trail
    public void SetMarbleTrail(int index)
    {
        switch(_currentState)
        {
            case GameState.PLAYER1_OPTIONS:
                _player1TrailIndex = index;
                break;

            case GameState.PLAYER2_OPTIONS:
                _player2TrailIndex = index;
                break;
        }
        _demoMarble.GetComponent<MarbleController>().SetTrail(_marbleTrails[index]);
    }

    //Displays the selected hat on the demo marble and updates the corresponding player's selected hat
    public void SetMarbleHat(int index)
    {
        switch(_currentState)
        {
            case GameState.PLAYER1_OPTIONS:
                _player1HatIndex = index;
                break;

            case GameState.PLAYER2_OPTIONS:
                _player2HatIndex = index;
                break;
        }
        _demoMarble.GetComponent<MarbleController>().SetHat(_marbleHats[index]);
    }

    //A menu has been "confirmed" and should be closed. The game state should transition baed upon what menu was just active.
    public void MenuClosed()
    {
        switch(_currentState)
        {
            case GameState.BOARD_SELECTION:
                _currentState = GameState.PLAYER1_OPTIONS;
                break;

            case GameState.PLAYER1_OPTIONS:
                _playerOptionsMenu.GetComponent<MenuVisibilityController>().Hide();

                Destroy(_demoMarble);
                Destroy(_playerOptionsMenu);

                _playerOptionsMenu = Instantiate(_playerOptionsMenuPrefab);
                _demoMarble = Instantiate(_playerMarblePrefab, new Vector3(_playerOptionsMenu.transform.position.x, -0.55f, _playerOptionsMenu.transform.position.z), Quaternion.identity, _playerOptionsMenu.transform);
                _demoMarble.AddComponent<MarbleOrbitController>();

                SetMarbleTexture(0);
                SetMarbleTrail(0);
                SetMarbleHat(0);

                _currentState = GameState.PLAYER2_OPTIONS;
                break;

            case GameState.PLAYER2_OPTIONS:
                _playerOptionsMenuPrefab.GetComponent<MenuVisibilityController>().Hide();

                Destroy(_demoMarble);
                Destroy(_playerOptionsMenu);

                _currentState = GameState.PLAYER1_TURN;
                SpawnPlayerMarble();
                break;
        }
    }

    //Instantiate a new player marble for the active player with their selected options.
    private void SpawnPlayerMarble()
    {
        GameObject marble = Instantiate(_playerMarblePrefab, _marbleStartPosition, Quaternion.identity);
        MarbleController script = marble.GetComponent<MarbleController>();

        switch (_currentState)
        {
            case GameState.PLAYER1_TURN:
                marble.GetComponent<Renderer>().material = _marbleTextures[_player1TextureIndex];
                script.SetTrail(_marbleTrails[_player1TrailIndex]);
                script.SetHat(_marbleHats[_player1HatIndex]);
                break;

            case GameState.PLAYER2_TURN:
                marble.GetComponent<Renderer>().material = _marbleTextures[_player2TextureIndex];
                script.SetTrail(_marbleTrails[_player2TrailIndex]);
                script.SetHat(_marbleHats[_player2HatIndex]);
                break;
        }
    }

    //Marble Controller designates the end of a player's turn
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

        //End of game
        if (_player2Marbles.Count >= 5)
        {
            _currentState = GameState.END_GAME;
            //int CalculateScore(_player1Marbles);
            //int CalculateScore(_player2Marbles);
        }
        else SpawnPlayerMarble();
    }
}