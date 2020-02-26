using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSelectionConfirmationController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private void OnMouseDown()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        //AudioSource.PlayClipAtPoint(_audioSource.clip, Camera.main.transform.position);
        //Gain access to the scene controller
        GameObject[] objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        SceneController sceneControllerScript = null;

        foreach (GameObject obj in objects)
        {
            //SceneController ctrl = obj.GetComponent<SceneController>();
            SceneController ctrl = obj.GetComponentInChildren<SceneController>();

            if (ctrl != null)
            {
                sceneControllerScript = ctrl;
                break;
            }
        }

        sceneControllerScript.MenuClosed();
    }
}
