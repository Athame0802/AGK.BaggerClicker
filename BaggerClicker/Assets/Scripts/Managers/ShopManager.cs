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
            D.Log($"방지권이 {input}개 구매됨");
            _mainManager.Preventer += input;
            _goldManager.Renewal();
        }

        else if ((string)product["productName"] == "MoveTo10")
        {
            D.Log($"+10강 이동권이 구매됨");
            _mainManager.MoveTo(_mainManager.SwordList[10]);
        }

        else if ((string)product["productName"] == "MoveToEnd")
        {
            D.Log($"종말로 향하는 별이 구매됨");
            _mainManager.MoveTo(_gameManager.EndSword);
        }

    }
}
