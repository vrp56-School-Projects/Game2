using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuConfirmationController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private void OnMouseDown()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        //AudioSource.PlayClipAtPoint(_audioSource.clip, Camera.main.transform.position);
        this.GetComponentInParent<PlayerOptionsController>().OptionsConfirmed();
    }
}
