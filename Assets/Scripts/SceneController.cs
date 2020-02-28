using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _stages;
    private GameObject _selectedStage;

    [SerializeField]
    private Material[] _marbleTextures;

    [SerializeField]
    private GameObject[] _marbleTrails;

    [SerializeField]
    private GameObject[] _marbleHats;

    [SerializeField]
    private GameObject _playerMarblePrefab;

    [SerializeField]
    private GameObject _demoMarblePrefab;

    [SerializeField]
    private GameObject _scoringMarble;

    private Vector3 _scoringMarbleStartPos;

    [SerializeField]
    private Vector3 _marbleStartPosition;

    [SerializeField]
    private Vector3 _menuSpawnPosition;

    [SerializeField]
    private Vector3 _comfirmSpawnPosition;
    private int _numMarbles = 5;

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
    private GameObject _boardSelectionMenuPrefab;

    [SerializeField]
    private GameObject _player1OptionsMenuPrefab;
    [SerializeField]
    private GameObject _player2OptionsMenuPrefab;
    [SerializeField]
    private GameObject _scoreViewPrefab;
    [SerializeField]
    private GameObject _outOfBoundsFailPrefab;

    private GameObject _activeMenu;

    public GameObject confirmButtonPrefab;
    private GameObject _confirmButton = null;

    [SerializeField]
    private int _redScore, _whiteScore, _blackScore, _outOfBoundsScore;

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
    private GameState _currentState = GameState.BOARD_SELECTION;

    // AR variables
    private PlaceOnPlane m_PlaceOnPlaneScript;

    // Start is called before the first frame update
    void Start()
    {
        _player1Marbles = new List<GameObject>();
        _player2Marbles = new List<GameObject>();
       

        m_PlaceOnPlaneScript = this.GetComponentInParent<PlaceOnPlane>();
        m_PlaceOnPlaneScript.Scale();

        _selectedStage = Instantiate(_stages[0]);
        // Should place the first stage in AR at hitpoint
        //_selectedStage = m_PlaceOnPlaneScript.Place(_stages[0]);
        _scoringMarble = _selectedStage.transform.Find("scoreMarblePrefab").gameObject;
        _scoringMarbleStartPos = _scoringMarble.transform.position;
        Debug.Log(_scoringMarbleStartPos);
        _activeMenu = Instantiate(_boardSelectionMenuPrefab, _menuSpawnPosition, Quaternion.identity, _selectedStage.transform);

        //Create and show the menu with the orbiting marble
        //_activeMenu = Instantiate(_player1OptionsMenuPrefab);
        //_demoMarble = Instantiate(_demoMarblePrefab, new Vector3(_activeMenu.transform.position.x, -0.55f, _activeMenu.transform.position.z), Quaternion.identity, _activeMenu.transform);
        //_demoMarble.AddComponent<MarbleOrbitController>();
        //SetMarbleTexture(0);
        //SetMarbleTrail(0);
        //SetMarbleHat(0);
        _activeMenu.GetComponent<MenuVisibilityController>().Show();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStage(int index)
    {
        Vector3 pos = _selectedStage.transform.position;
        Quaternion quat = _selectedStage.transform.rotation;

        //try to keep active menu alive
        _activeMenu.transform.SetParent(this.transform);

        Destroy(_selectedStage);

        _selectedStage = Instantiate(_stages[index], pos, quat);
        // Should place the first stage in AR at hitpoint
        //_selectedStage = m_PlaceOnPlaneScript.Place(_stages[index]); 

        _scoringMarble = _selectedStage.transform.Find("scoreMarblePrefab").gameObject;
        _scoringMarbleStartPos = _scoringMarble.transform.position;
        Debug.Log(_scoringMarbleStartPos);

        //reparent the active menu
        _activeMenu.transform.SetParent(_selectedStage.transform);
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
                _activeMenu.GetComponent<MenuVisibilityController>().Hide();
                Destroy(_activeMenu, 0.25f);

                _activeMenu = Instantiate(_player1OptionsMenuPrefab, _menuSpawnPosition, Quaternion.identity, _selectedStage.transform);
                _demoMarble = Instantiate(_demoMarblePrefab, new Vector3(_activeMenu.transform.position.x, -0.55f, _activeMenu.transform.position.z), Quaternion.identity, _activeMenu.transform);
                _demoMarble.AddComponent<MarbleOrbitController>();

                _currentState = GameState.PLAYER1_OPTIONS;

                SetMarbleTexture(0);
                SetMarbleTrail(0);
                SetMarbleHat(0);

                _activeMenu.GetComponent<MenuVisibilityController>().Show();
                break;

            case GameState.PLAYER1_OPTIONS:

                _activeMenu.GetComponent<MenuVisibilityController>().Hide();

                Destroy(_demoMarble);
                Destroy(_activeMenu, 0.25f);

                _activeMenu = Instantiate(_player2OptionsMenuPrefab, _menuSpawnPosition, Quaternion.identity, _selectedStage.transform);
                _demoMarble = Instantiate(_demoMarblePrefab, new Vector3(_activeMenu.transform.position.x, -0.55f, _activeMenu.transform.position.z), Quaternion.identity, _activeMenu.transform);
                _demoMarble.AddComponent<MarbleOrbitController>();

                _currentState = GameState.PLAYER2_OPTIONS;

                SetMarbleTexture(0);
                SetMarbleTrail(0);
                SetMarbleHat(0);

                _activeMenu.GetComponent<MenuVisibilityController>().Show();
                break;

            case GameState.PLAYER2_OPTIONS:
                _activeMenu.GetComponent<MenuVisibilityController>().Hide();

                Destroy(_demoMarble);
                Destroy(_activeMenu, 0.25f);

                _currentState = GameState.PLAYER1_TURN;
                SpawnPlayerMarble();
                break;

            case GameState.END_GAME:
                _activeMenu.GetComponent<MenuVisibilityController>().Hide();
                Destroy(_activeMenu, 0.25f);

                foreach(GameObject obj in _player1Marbles) Destroy(obj);

                foreach (GameObject obj in _player2Marbles) Destroy(obj);

                _player1Marbles.Clear();
                _player2Marbles.Clear();

                _currentState = GameState.BOARD_SELECTION;

                

                _activeMenu = Instantiate(_boardSelectionMenuPrefab, _menuSpawnPosition, Quaternion.identity, _selectedStage.transform);
                SetStage(0);

                _activeMenu.GetComponent<MenuVisibilityController>().Show();
                break;
        }
    }

    //Instantiate a new player marble for the active player with their selected options.
    private void SpawnPlayerMarble()
    {
        GameObject marble = Instantiate(_playerMarblePrefab, _marbleStartPosition, Quaternion.identity);

        if (_confirmButton != null)
        {
            Destroy(_confirmButton);
        }

        _confirmButton = Instantiate(confirmButtonPrefab, _comfirmSpawnPosition, Quaternion.identity);
        
        MarbleController script = marble.GetComponent<MarbleController>();
        _confirmButton.GetComponent<MarbleConfirmation>().SetMarbleController(script);


        switch (_currentState)
        {
            case GameState.PLAYER1_TURN:
                marble.GetComponent<Renderer>().material = _marbleTextures[_player1TextureIndex];
                script.SetTrail(_marbleTrails[_player1TrailIndex]);
                script.SetHat(_marbleHats[_player1HatIndex]);
                _player1Marbles.Add(marble);
                break;

            case GameState.PLAYER2_TURN:
                marble.GetComponent<Renderer>().material = _marbleTextures[_player2TextureIndex];
                script.SetTrail(_marbleTrails[_player2TrailIndex]);
                script.SetHat(_marbleHats[_player2HatIndex]);
                _player2Marbles.Add(marble);
                break;
        }
       


    }

    //Marble Controller designates the end of a player's turn
    public void EndTurn()
    {
        Destroy(_confirmButton);

        switch (_currentState)
        {
            case GameState.PLAYER1_TURN:
                if (Vector3.Distance(_scoringMarbleStartPos, _scoringMarble.transform.position) > 1.05f)
                {
                    _currentState = GameState.END_GAME;

                    _activeMenu = Instantiate(_outOfBoundsFailPrefab);
                    OutOfBoundsFailController script = _activeMenu.GetComponent<OutOfBoundsFailController>();

                    script.SetWinner("Player 2");
                    script.SetLoser("Player 1");
                    return;
                } else
                {
                    _currentState = GameState.PLAYER2_TURN;
                }
                break;

            case GameState.PLAYER2_TURN:
                if (Vector3.Distance(_scoringMarbleStartPos, _scoringMarble.transform.position) > 1.05f)
                {
                    _currentState = GameState.END_GAME;

                    _activeMenu = Instantiate(_outOfBoundsFailPrefab);
                    OutOfBoundsFailController script = _activeMenu.GetComponent<OutOfBoundsFailController>();

                    script.SetWinner("Player 1");
                    script.SetLoser("Player 2");
                    return;
                }
                else
                {
                    _currentState = GameState.PLAYER1_TURN;
                }
                break;
        }

        //End of game
        if (_player2Marbles.Count >= _numMarbles)
        {
            _currentState = GameState.END_GAME;

            _activeMenu = Instantiate(_scoreViewPrefab, _menuSpawnPosition, Quaternion.identity, _selectedStage.transform);
            ScoreViewController script = _activeMenu.GetComponent<ScoreViewController>();            

            int score1 = CalculateScore(_player1Marbles);
            int score2 = CalculateScore(_player2Marbles);

            script.SetPlayer1Score(score1);
            script.SetPlayer2Score(score2);

            if (score1 < score2)
            {
                script.SetWinner("Player 1 Wins!");
            }
            else if (score2 < score1)
            {
                script.SetWinner("Player 2 Wins!");
            } else if (score1==score2)
            {
                script.SetWinner("It's a tie!");
            }
        }
        else SpawnPlayerMarble();
    }

    public int CalculateScore(List<GameObject> playerMarbles)
    {
        int _points = 0;
        for (int i = 0; i < playerMarbles.Count; i++)
        {
            float _individualDistance = Vector3.Distance(_scoringMarble.transform.position, playerMarbles[i].transform.position);
            if (_individualDistance < 0.4f)
            {
                _points += _redScore;
            } else if (_individualDistance < 0.75f && _individualDistance > 0.4f)
            {
                _points += _whiteScore;
            } else if (_individualDistance < 1.05f && _individualDistance > 0.75f)
            {
                _points += _blackScore;
            } else
            {
                _points += _outOfBoundsScore;
            }
            
        }
        return _points;
    }
}