using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanel;
    private BalanceHandler _balanceHandler;
    private bool _canCloseGamePanel;
    public event Action startGame = default;
    public event Action<bool> canOpenCase = default;

    private void OnEnable()
    {
        FindObjectOfType<CaseHandler>().caseChoosed += OnCaseChoosed;
    }

    private void OnDisable()
    {
        FindObjectOfType<CaseHandler>().caseChoosed -= OnCaseChoosed;
    }

    private void Awake()
    {
        _balanceHandler = FindObjectOfType<BalanceHandler>();
    }

    public void OnGamePanelOnBtnClick()
    {
        if(_balanceHandler.Sum >= 50)
        {
            canOpenCase(true);
            _gamePanel.SetActive(true);
            startGame();
        }
    }

    public void OffGamePanelBtnClick()
    {
        if(_canCloseGamePanel == true)
        {
            canOpenCase(false);
            _gamePanel.SetActive(false);
            _canCloseGamePanel = false;
        }
    }

    private void OnCaseChoosed(bool caseChoosed)
    {
        _canCloseGamePanel = caseChoosed;
    }
}
