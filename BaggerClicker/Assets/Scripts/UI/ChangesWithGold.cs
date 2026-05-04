using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class ChangesWithGold : MonoBehaviour
{
    private GoldManager _goldManager;
    private TMP_Text _textComponent;

    void Awake()
    {
        _goldManager = FindAnyObjectByType<GoldManager>();
        _textComponent = gameObject.GetComponent<TMP_Text>();

        if (_goldManager == null)
        {
            D.LogError("_goldManager를 찾을 수 없음", this);
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
        _textComponent.text = "돈 : " + _goldManager.Gold;
    }
}
