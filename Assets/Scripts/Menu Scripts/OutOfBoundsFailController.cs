using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsFailController : MonoBehaviour
{
    [SerializeField]
    private GameObject _winnerTitle;

    [SerializeField]
    private GameObject _loserTitle;


    public void SetWinner(string winner)
    {
        _winnerTitle.GetComponent<TMPro.TextMeshPro>().text = winner + " Wins!";
    }

    public void SetLoser(string loser)
    {
        _loserTitle.GetComponent<TMPro.TextMeshPro>().text = loser + " knocked the scoring marble out of bounds";
    }

}
