using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// IIndexable을 상속 받는 데이터를 넣고 UI에서 출력을 원하는 값을 넣으면 출력됨

public class ChangesWithAsset : MonoBehaviour
{
    [SerializeField] protected string dataFieldName;
    [SerializeField] protected IIndexable data;
    protected object _dataField;
    protected object _changeContent;

    protected virtual void Start()
    {
        D.Log($"{gameObject}에서 {data}의 {dataFieldName} 불러오는 중...", this);
        _dataField = data[dataFieldName];

        if (_dataField == null || _dataField is IIndexable)
        {
            D.LogError($"잘못된 입력입니다! 이는 심각한 오류를 발생시킬 수 있습니다. 현재 접근 : {data}[{dataFieldName}], 담긴 값 : {_dataField}", this);
        }


        if (_dataField is Sprite)
        {
            _changeContent = GetComponent<Image>().sprite;
        }
        else
        {
            _changeContent = GetComponent<TMP_Text>().text;
        }

        Renewal();
    }

    protected void Renewal()
    {
        if (_changeContent is Sprite)
        {
            _changeContent = data[dataFieldName];
        }
        else
        {
            _changeContent = (string)_changeContent + data[dataFieldName];
        }
    }
}
