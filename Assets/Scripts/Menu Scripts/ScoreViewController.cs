using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _winnerTitle;

    [SerializeField]
    private GameObject _player1Score;

    [SerializeField]
    private GameObject _player2Score;

    public void SetWinner(string winner)
    {
        _winnerTitle.GetComponent<TMPro.TextMeshPro>().text = winner;
    }

    public void SetPlayer1Score(int score)
    {
        _player1Score.GetComponent<TMPro.TextMeshPro>().text = "Player 1: " + score.ToString();
    }

    public void SetPlayer2Score(int score)
    {
        _player2Score.GetComponent<TMPro.TextMeshPro>().text = "Player 2: " + score.ToString();
    }
}