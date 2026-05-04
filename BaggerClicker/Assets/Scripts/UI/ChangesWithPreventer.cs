using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangesWithPreventer : MonoBehaviour
{
    private MainManager _mainManager;
    private TMP_Text _textComponent;

    void Awake()
    {
        _mainManager = FindAnyObjectByType<MainManager>();
        _textComponent = gameObject.GetComponent<TMP_Text>();

        if (_mainManager == null)
        {
            D.LogError("_mainManager를 찾을 수 없음", this);
        }
        else if (_textComponent == null)
        {
            D.LogError("_textComponent를 찾을 수 없음", this);
        }

        Renewal();
        GoldManager.RenewalEvent += new Action(Renewal);
    }

    void OnDestroy()
    {
        GoldManager.RenewalEvent -= Renewal;
    }

    private void Renewal()
    {
        _textComponent.text = "방지권 : " + _mainManager.Preventer;
    }
}
