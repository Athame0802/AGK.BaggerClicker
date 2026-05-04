using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private GoldManager _goldManager;
    private MainManager _mainManager;
    private GameManager _gameManager;

    void Awake()
    {
        _goldManager = FindAnyObjectByType<GoldManager>();
        _mainManager = FindAnyObjectByType<MainManager>();
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    public void Purchase(int input, Product product)
    {
        if(!_goldManager.GoldMinus((ulong)product["productPrice"] * (ulong)input))
        {
            return;
        }

        if ((string)product["productName"] == "Preventer")
        {
            _mainManager.Preventer += input;
        }

        else if ((string)product["productName"] == "MoveTo10")
        {
            _mainManager.MoveTo(_mainManager.SwordList[10]);
        }

        else if ((string)product["productName"] == "MoveToEnd")
        {
            _mainManager.MoveTo(_gameManager.EndSword);
        }

    }
}
