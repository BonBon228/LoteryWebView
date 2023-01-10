using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseHandler : MonoBehaviour
{
    private enum ResultsSelector
    {
        Won,
        Nothing,
        Lost,
        Bonus
    }

    public event System.Action<int> isWon = default;
    public event System.Action<bool> caseChoosed = default;
    private int _randomValue;
    private int _currentTry = 0;
    private ResultsSelector results;
    private bool _canOpen = false;

    private void OnEnable() 
    {
        FindObjectOfType<GameStarter>().canOpenCase += OnCanOpenCase;
    }

    private void OnDisable()
    {
        FindObjectOfType<GameStarter>().canOpenCase -= OnCanOpenCase;
    }

    private void Start()
    {
        caseChoosed(false);
    }

    private void Update()
    {
        if(_canOpen == true)
        {
            CaseHit();
        }
    }

    private void OpenTryCounter()
    {
        _currentTry += 1;
        PlayerPrefs.SetInt("CurrentTry", _currentTry);
        if( _currentTry == 5)
        {
            _currentTry = 0;
        }
    }

    private void Randomizer()
    {
        if(_currentTry < 4)
        {
            _randomValue = Random.Range(0,2);
            results = (ResultsSelector)_randomValue;
        }
        else if(_currentTry == 4)
        {
            _randomValue = Random.Range(0, 100);
            if(_randomValue == 100)
            {
                results = ResultsSelector.Bonus;
            }
            else
            {
                results = ResultsSelector.Lost;
            }
        }
    }

    private void OnCanOpenCase(bool canOpen)
    {
        _canOpen = canOpen;
    }

    private void CaseHit()
    {
        Vector2 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(curMousePos, Vector2.zero);
        if(Input.GetMouseButtonDown(0) && rayHit.collider.CompareTag("Case"))
        {
            Randomizer();
            isWon((int)results);
            caseChoosed(true);
            OpenTryCounter();
            _canOpen = false;
        }
    }
}
