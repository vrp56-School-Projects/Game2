using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOptionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject _boardOption;

    private SceneController _sceneControllerScript;

    private CycleOptionController _boardOptionController;

    // Start is called before the first frame update
    void Start()
    {
        //Gain access to the scene controller
        GameObject[] objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in objects)
        {
            SceneController ctrl = obj.GetComponent<SceneController>();

            if (ctrl != null)
            {
                _sceneControllerScript = ctrl;
                break;
            }
        }

        _boardOptionController = _boardOption.GetComponent<CycleOptionController>();
    }

    public void UpdateBoard()
    {
        int index = _boardOptionController.GetCurrentIndex();

        _sceneControllerScript.SetStage(index);
    }
}