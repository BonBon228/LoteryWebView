using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _sumText;
    public int Sum { get; private set; }

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

    private void Start()
    {
        GetSum();
        SetSumText();
    }

    private void OnIsWon(int results)
    {
        switch(results)
        {
            case 0:
                Sum += 60;
                SaveSum();
                break;
            case 1:
                Sum = Sum;
                SaveSum();
                break;
            case 2:
                Sum -= 10;
                SaveSum();
                break;
            case 3:
                Sum += 500;
                SaveSum();
                break;
        }
    }

    private void OnStartGame()
    {
        Sum -= 50;
        SaveSum();
    }

    private void GetSum()
    {
        if(PlayerPrefs.HasKey("Balance"))
        {
            Sum = PlayerPrefs.GetInt("Balance");
        }
        else
        {
            Sum = 500;
            PlayerPrefs.SetInt("Balance", Sum);
        }
    }

    private void SaveSum()
    {
        PlayerPrefs.SetInt("Balance", Sum);
        SetSumText();
    }

    private void SetSumText()
    {
        _sumText.SetText("Coins: " + PlayerPrefs.GetInt("Balance"));
    }
}
