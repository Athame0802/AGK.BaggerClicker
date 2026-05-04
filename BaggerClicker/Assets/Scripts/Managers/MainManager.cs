using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static event Action RenewalEvent;
    public static event Action ToNextSwordEvent;
    public static event Action ToFirstSwordEvent;
    public static event Action<Sword> MoveToEvent;

    public static Enhanceable enhanceableComponent { get; private set; }
    private GameManager _gameManager;
    private GoldManager _goldManager;
    private Toggle _preventerToggle;
    private GameObject _makesMoneyButton;

    public List<Sword> SwordList = new(20);
    public int Preventer { get; set; }

    void Awake()
    {
        enhanceableComponent = FindAnyObjectByType<Enhanceable>();
        _gameManager = FindAnyObjectByType<GameManager>();
        _goldManager = FindAnyObjectByType<GoldManager>();
        _preventerToggle = FindAnyObjectByType<Toggle>();
        _makesMoneyButton = GameObject.Find("Makes Money Button");

        if (_makesMoneyButton == null)
        {
            D.LogError("_makesMoneyButton을 찾을 수 없습니다!", this);
        }
    }

    void Start()
    {
        _makesMoneyButton.SetActive(false);
    }

    public void Enhance()
    {
        if (_preventerToggle.isOn == true && Preventer <= 0)
        {
            D.Log("방지권 사용이 체크되었지만 방지권이 없음", this);
            _gameManager.ShowErrorText("방지권이 없습니다!");
            return;
        }
        
        if (!_goldManager.GoldMinus((ulong)enhanceableComponent.data["enhanceCost"]))
        {
            return;
        }
        
        int random = UnityEngine.Random.Range(1, 101);
        D.Log($"산출된 숫자 : {random}", this);

        if (random <= (int)enhanceableComponent.data["enhanceProbability"])
        {
            ToNextSwordEvent?.Invoke();
            enhanceableComponent.data = (Sword)FindAnyObjectByType<Enhanceable>().data;
            _gameManager.EndCheck();

            D.Log($"강화 성공! {enhanceableComponent.data}로 올라갑니다.", this);
        }

        else
        {
            if (_preventerToggle.isOn == true)
            { 
                D.Log("파괴 방지권으로 강화 유지", this);
                Preventer--;
                return;
            }

            ToFirstSwordEvent?.Invoke();
            enhanceableComponent.data = (Sword)FindAnyObjectByType<Enhanceable>().data;

            D.Log("강화 실패... 처음으로 돌아갑니다.", this);

            if (_goldManager.Gold < 500)
            {
                _makesMoneyButton.SetActive(true);
            }
        }

        RenewalEvent?.Invoke();
        
    }

    public void MoveTo(Sword sword)
    {
        MoveToEvent?.Invoke(sword);
        RenewalEvent?.Invoke();
        _gameManager.EndCheck();
    }

    public void Sell()
    {
        _goldManager.GoldPlus((ulong)enhanceableComponent.data["sellValue"]);

        ToFirstSwordEvent?.Invoke();
        RenewalEvent?.Invoke();

        D.Log("검 판매됨", this);
    }
}
