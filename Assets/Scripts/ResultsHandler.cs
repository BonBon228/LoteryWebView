using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text resultsText;

    private void OnEnable() 
    {
        FindObjectOfType<CaseHandler>().isWon += OnIsWon;
        FindObjectOfType<GameStarter>().startGame += OnStartGame;
    }

    private void OnDisable()
    {
        FindObjectOfType<CaseHandler>().isWon -= OnIsWon;
        FindObjectOfType<GameStarter>().startGame -= OnStartGame;
    }

    private void OnStartGame()
    {
        resultsText.SetText("");
    }

    private void OnIsWon(int results)
    {
        switch(results)
        {
            case 0:
                resultsText.SetText("+10!");
                break;
            case 1:
                resultsText.SetText("Nothing");
                break;
            case 2:
                resultsText.SetText("-10");
                break;
            case 3:
                resultsText.SetText("BONUS: +500!!!");
                break;
        }
    }
}
