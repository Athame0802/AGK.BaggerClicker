using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static event Action ToNextSwordEvent;
    public static event Action ToFirstSwordEvent;
    public static event Action RenewalEvent;

    public static Sword currentSword { get; private set; }
    private GameManager _gameManager;
    private GoldManager _goldManager;

    void Start()
    {
        currentSword = (Sword)FindAnyObjectByType<Enhanceable>().data;
        _gameManager = FindAnyObjectByType<GameManager>();
        _goldManager = FindAnyObjectByType<GoldManager>();
    }


    public void Enhance()
    {
        if (!_goldManager.GoldMinus((int)currentSword["enhanceCost"]))
        {
            return;
        }

        int random = UnityEngine.Random.Range(1, 101);

        if (random <= (int)currentSword["enhanceProbability"])
        {
            ToNextSwordEvent?.Invoke();
            currentSword = (Sword)FindAnyObjectByType<Enhanceable>().data;
            _gameManager.EndCheck();

            D.Log($"강화 성공! {currentSword}로 올라갑니다.", this);
        }

        else
        {
            ToFirstSwordEvent?.Invoke();
            currentSword = (Sword)FindAnyObjectByType<Enhanceable>().data;

            D.Log("강화 실패... 처음으로 돌아갑니다.", this);
        }

        RenewalEvent?.Invoke();
        
    }

    public void Sell()
    {
        _goldManager.GoldPlus((int)currentSword["sellValue"]);

        ToFirstSwordEvent?.Invoke();
        RenewalEvent?.Invoke();
    }
}
