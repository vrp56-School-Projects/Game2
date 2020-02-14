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
    private GameObject _marblePrefab;

    [SerializeField]
    private Vector3 _marbleStartPosition;

    private List<GameObject> _player1Marbles;
    private List<GameObject> _player2Marbles;

    private GameObject _demoMarble;

    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        _player1Marbles = new List<GameObject>();
        _player2Marbles = new List<GameObject>();

        _demoMarble = Instantiate(_marblePrefab, new Vector3(Menu.transform.position.x, -0.55f, Menu.transform.position.z), Quaternion.identity, Menu.transform);
        _demoMarble.AddComponent<MarbleOrbitController>();

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
}
