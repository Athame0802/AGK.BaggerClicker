using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// IIndexable을 상속 받는 데이터를 넣고 UI에서 출력을 원하는 값을 넣으면 출력됨

public class ChangesWithAsset : MonoBehaviour
{
    public IIndexable data;
    [SerializeField] private ScriptableObject rawSO;
    [SerializeField] protected string dataFieldName;
    [SerializeField] private bool isAllChange = false;
    [SerializeField] private bool isSwordName = false;
    protected IIndexable _data;
    protected object _dataField;
    protected object _changeContent;
    protected string originText;

    protected virtual void Awake()
    {
        if (rawSO is IIndexable indexableData)
        {
            data = indexableData;
        }
        else
        {
            D.LogError($"잘못된 rawSO 입력입니다! 이는 심각한 오류를 발생시킬 수 있습니다. 현재 rawSO : {rawSO}");
        }

        _data = data;

        D.Log($"{gameObject}에서 {_data}의 {dataFieldName} 불러오는 중...", this);
        _dataField = _data[dataFieldName];

        if (_dataField == null || _dataField is IIndexable)
        {
            D.LogError($"잘못된 dataFieldName 입력입니다! 이는 심각한 오류를 발생시킬 수 있습니다. 현재 접근 : {_data}[{dataFieldName}], 담긴 값 : {_dataField}", this);
        }

        D.Log($"{gameObject}에서 {_data}의 {dataFieldName}를 불러오는데 성공함!", this);

        if (_dataField is Sprite)
        {
            _changeContent = GetComponent<Image>();
        }
        else
        {
            _changeContent = GetComponent<TMP_Text>();
            originText = GetComponent<TMP_Text>().text;
        }


        Renewal();
    }

    protected void Renewal()
    {
        if (_changeContent is Image img)
        {
            img.sprite = (Sprite)_data[dataFieldName];
        }
        else if (_changeContent is TMP_Text tmpText)
        {
            if (!isAllChange)
            {
                tmpText.text = originText + _data[dataFieldName];
            }
            
            else
            {
                if (isSwordName)
                {
                    tmpText.text = $"+{_data["enhanceCount"]} {_data[dataFieldName]}";
                    return;
                }

                tmpText.text = (string)_data[dataFieldName];
            }
        }
    }
}
