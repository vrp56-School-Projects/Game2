using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuConfirmationController : MonoBehaviour
{
    private void OnMouseDown()
    {
        this.GetComponentInParent<PlayerOptionsController>().OptionsConfirmed();
    }
}
