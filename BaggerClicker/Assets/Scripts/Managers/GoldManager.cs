using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static event Action RenewalEvent;

    private GameManager _gameManager;


    public ulong Gold { get; private set; }

    void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();

    }

    public void GoldPlus(ulong amount) 
    {
        if (ulong.MaxValue - Gold < amount)
        {
            D.Log("골드 최대치에 도달했습니다!");

            Gold = ulong.MaxValue - 1;
            _gameManager.ShowErrorText($"골드 최대치에 도달했습니다!");

            RenewalEvent?.Invoke();
            
            return;
        }

        Gold += amount;
        RenewalEvent?.Invoke();
    }

    public bool GoldMinus(ulong amount)
    {
        if (Gold < amount)
        {
            D.Log($"골드가 부족합니다! 현재 골드 : {Gold}");

            _gameManager.ShowErrorText($"골드가 부족합니다!");

            return false;
        }

        Gold -= amount;
        RenewalEvent?.Invoke();

        

        return true;
    }

    public void MakesMoney()
    {
        GoldPlus(100);
    }
}
