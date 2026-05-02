using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold { get; private set; }
    
    public void GoldPlus(int amount) 
    {
        gold += amount;
    }

    public bool GoldMinus(int amount)
    {
        if (gold < amount)
        {
            D.Log($"골드가 부족합니다! 현재 골드 : {gold}");
            
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            gameManager.ShowErrorText($"골드가 부족합니다!");

            return false;
        }

        gold -= amount;

        return true;
    }
}
