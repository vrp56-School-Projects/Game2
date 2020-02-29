using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleConfirmation : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    private MarbleController _marbleController;

    private void OnMouseDown()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        //AudioSource.PlayClipAtPoint(_audioSource.clip, Camera.main.transform.position);
        //Gain access to the scene controller

        _marbleController.buttonPressed = true;

    }

    public void SetMarbleController(MarbleController marbleController)
    {
        _marbleController = marbleController;
    }

    private void Update()
    {
        //Vector3 buttonPos = this.transform.localPosition;
        //float x = Camera.main.transform.position.x;

        //buttonPos.x = x;
        //this.transform.localPosition = buttonPos;
    }
}
