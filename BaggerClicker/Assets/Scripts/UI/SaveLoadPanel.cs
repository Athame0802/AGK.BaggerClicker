using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadPanel : MonoBehaviour
{
    public bool isSave;
    private TMP_Text _saveLoadTitleText;
    private GameObject _saveCheckPanel;
    private GameObject[] _saveButtons = new GameObject[3];
    private Button[] _saveButtonsButton = new Button[3];
    private TMP_Text[] _saveDescriptionsText = new TMP_Text[3];

    void Awake()
    {
        _saveLoadTitleText = transform.GetChild(0).GetComponent<TMP_Text>();
        _saveCheckPanel = transform.GetChild(5).gameObject;
        
        for (int i = 0; i < _saveButtons.Length; i++)
        {
            _saveButtons[i] = transform.GetChild(i + 1).gameObject;
            _saveButtonsButton[i] = _saveButtons[i].GetComponent<Button>();
            _saveDescriptionsText[i] = _saveButtons[i].transform.GetChild(1).GetComponent<TMP_Text>();
            D.Log($"{i}번째 : _saveButton : {_saveButtons[i]}, _saveButtonsButton : {_saveButtonsButton[i]}, _saveDescriptionText : {_saveDescriptionsText[i]}");
        }
    }

    void Start()
    {
        SetSaveLoadPanelActive(false);
        SetSaveCheckPanelActive(false);
    }

    public void SetSaveLoadPanelActive(bool isOn)
    {
        gameObject.SetActive(isOn);

        if (isSave)
        {
            _saveLoadTitleText.text = "저장";
        }
        else
        {
            _saveLoadTitleText.text = "불러오기";
        }

        Renewal();
    }

    public void SetSaveButtonActive(int index, bool isOn)
    {
        _saveButtonsButton[index].interactable = isOn;
    }

    public void SetDescription(int index, string text)
    {
        _saveDescriptionsText[index].text = text;
    }

    public void SetSaveCheckPanelActive(bool isOn)
    {
        _saveCheckPanel.SetActive(isOn);
    }

    public void OnSaveLoadButtonClicked(int index)
    {
        if (isSave)
        {
            SaveManager.Instance.SaveCheck(index);
        }
        else
        {
            SaveManager.Instance.Load(index);
        }
    }

    public void Renewal()
    {
        D.Log("SaveLoadPanel의 Renewal() 실행됨");

        if (isSave) // 저장 기능을 수행해야 하면
        {
            for(int i = 0; i < _saveButtons.Length; i++)
            {
                SetSaveButtonActive(i, true); // 모든 버튼 키기
            }
        }

        int index = 0; // index 선언 (세이브 파일)

        foreach (TMP_Text descriptionText in _saveDescriptionsText)
        {
            if (SaveManager.Instance.SaveArray[index] == null || SaveManager.Instance.SaveArray[index].isSaved == false) // null 이거나 isSaved가 false라면
            {
                if (!isSave) // 파일이 없고 불러오기 기능을 수행해야 하면
                {
                    SetSaveButtonActive(index, false); // 해당 세이브 파일 불러오는 버튼 끄기
                }

                descriptionText.text = "저장된 파일 없음"; // 저장 기능이든 불러오기 기능이든 없다고 표시

                index++; // 다음 파일 검사
                continue;
            }

            // null이 아니고 isSaved가 true라면
            descriptionText.text = $"돈 : {SaveManager.Instance.SaveArray[index].gold}원 / +{SaveManager.Instance.SaveArray[index].currentSword["enhanceCount"]} {SaveManager.Instance.SaveArray[index].currentSword["swordName"]} / 플레이 타임 : {(int)(SaveManager.Instance.SaveArray[index].time / 60)}";
            
            index++;
        }
    }
}
