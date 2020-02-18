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
    private GameObject _scoringMarblePrefab;

    [SerializeField]
    private Vector3 _marbleStartPosition;

    private List<GameObject> _player1Marbles;
    private List<GameObject> _player2Marbles;

    private GameObject _demoMarble;

    [SerializeField]
    private GameObject _playerOptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        _player1Marbles = new List<GameObject>();
        _player2Marbles = new List<GameObject>();

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

    private void SpawnPlayerMarble()
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
        _playerOptionsMenu.GetComponent<MenuVisibilityController>().Hide();
    }

    public int CalculateScore(List<GameObject> playerMarbles)
    {
        int _points = 0;
        for (int i = 0; i < playerMarbles.Count; i++)
        {
            //For each marble, add their distances to the scoring marble together
            float _individualDistance = Vector3.Distance(_scoringMarblePrefab.transform.position, playerMarbles[i].transform.position);
            if (_individualDistance < 0.4f)
            {
                _points += 5;
            } else if (_individualDistance < 0.75f && _individualDistance > 0.4f)
            {
                _points += 3;
            } else if (_individualDistance < 1.05f && _individualDistance > 0.75f)
            {
                _points += 1;
            }
            //No points if outside 1.05 units, or out of bounds. So do nothing.
        }
        return _points;
    }
}
