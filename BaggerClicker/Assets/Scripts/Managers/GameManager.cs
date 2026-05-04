using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject _start;
    private GameObject _main;
    private GameObject _shop;
    private GameObject _end;

    private GameObject _endButton;
    private GameObject _enhanceButton;
    private GameObject _sellButton;
    private GameObject _endText;
    private GameObject _errorText;
    private GameObject _preventerToggle;
    private TMP_Text _errorTextComponent;
    private Button _enhanceButtonComponent;
    private Button _sellButtonComponent;
    public GameObject makesMoneyButton { get; private set; }
    private GameObject[] _purchaseButtons = new GameObject[3];
    private Button[] _purchaseButtonsComponent = new Button[3];

    public Sword EndSword;

    private GoldManager _goldManager;

    public double Time;


    void Awake()
    {
        _start = GameObject.Find("Start");
        _main = GameObject.Find("Main");
        _shop = GameObject.Find("Shop");
        _end = GameObject.Find("End");

        _endText = GameObject.Find("End Text");
        _endButton = GameObject.Find("End Button");
        _enhanceButton = GameObject.Find("Enhance Button");
        _sellButton = GameObject.Find("Sell Button");
        _errorText = GameObject.Find("Error Text");
        _preventerToggle = GameObject.Find("Preventer Toggle");
        makesMoneyButton = GameObject.Find("Makes Money Button");

        _goldManager = FindAnyObjectByType<GoldManager>();
        _purchaseButtons = GameObject.FindGameObjectsWithTag("PurchaseButton");

        if (_start == null || _endButton == null || _main == null || _shop == null || _enhanceButton == null || _end == null || _endButton == null || _errorText == null || _goldManager == null || _preventerToggle == null || makesMoneyButton == null)
        {
            D.LogError("게임오브젝트를 찾을 수 없습니다!", this);
        }

        _errorTextComponent = _errorText.GetComponent<TMP_Text>();
        _enhanceButtonComponent = _enhanceButton.GetComponent<Button>();
        _sellButtonComponent = _sellButton.GetComponent<Button>();
        
        int index = 0;
        foreach (GameObject purchaseButton in _purchaseButtons)
        {
            _purchaseButtonsComponent[index] = purchaseButton.GetComponent<Button>();
            index++;
        }

        if (_errorTextComponent == null || _enhanceButtonComponent == null || _sellButtonComponent == null)
        {
            D.LogError("컴포넌트를 찾을 수 없습니다!", this);
        }
    }

    void Start()
    {
        _main.SetActive(false);
        _shop.SetActive(false);
        _endButton.SetActive(false);
        _end.SetActive(false);
        _errorText.SetActive(false);
        makesMoneyButton.SetActive(false);
    }

    void Update()
    {
        Time += UnityEngine.Time.deltaTime;
    }

    public void MoveForStart()
    {
        D.Log("MoveForStart 실행됨", this);

        _start.SetActive(false);
        _main.SetActive(true);
    }

    public void MoveToShop()
    {
        D.Log("MoveToShop 실행됨", this);

        _main.SetActive(false);
        _shop.SetActive(true);
    }

    public void MoveToMain()
    {
        D.Log("MoveToMain 실행됨", this);

        _shop.SetActive(false);
        _main.SetActive(true);
    }

    public void EndCheck()
    {
        if ((Sword)MainManager.enhanceableComponent.data == EndSword)
        {
            _endButton.SetActive(true);
            _enhanceButton.SetActive(false);
            _sellButton.SetActive(false);
            _preventerToggle.SetActive(false);
        }
    }

    public void End()
    {
        _main.SetActive(false);
        _shop.SetActive(false);
        _end.SetActive(true);

        _endText.GetComponent<TMP_Text>().text = $"- END - \r\n최종 골드 : {_goldManager.Gold}\r\n총 플레이 타임 : {(int)(Time / 60)}분\r\n\r\n플레이해 주셔서 감사합니다.";
    }

    public void ShowErrorText(string message)
    {
        StartCoroutine(PrintErrorText(message));

        if (_goldManager.Gold < 500)
        {
            makesMoneyButton.SetActive(true);
        }
    }

    private IEnumerator PrintErrorText(string message)
    {
        _errorText.SetActive(true);
        _errorTextComponent.text = message;

        _enhanceButtonComponent.interactable = false;
        _sellButtonComponent.interactable = false;
        foreach (Button purchaseButtonComponent in _purchaseButtonsComponent)
        {
            purchaseButtonComponent.interactable = false;
        }

        yield return new WaitForSecondsRealtime(1.5f); // 1.5초 동안 멈추기

        _errorText.SetActive(false);

        _enhanceButtonComponent.interactable = true;
        _sellButtonComponent.interactable = true;
        foreach (Button purchaseButtonComponent in _purchaseButtonsComponent)
        {
            purchaseButtonComponent.interactable = true;
        }
    }

    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void SetMakesMoneyButtonActive(bool isOn)
    {
        makesMoneyButton.SetActive(isOn);
    }
}
