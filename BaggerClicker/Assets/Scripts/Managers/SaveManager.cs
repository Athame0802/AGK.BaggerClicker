using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public ulong gold;
    public int preventer;
    public double time;
    public Sword currentSword;
    public bool isMakesMoneyButtonOn;
    public bool isSaved = false;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private GameManager _gameManager;
    private MainManager _mainManager;
    private GoldManager _goldManager;
    private Enhanceable _anyEnhanceable;
    private SaveLoadPanel _saveLoadPanel;

    public SaveData[] SaveArray = new SaveData[3];
    public SaveData CurrentSave = new();

    private string _path;
    private readonly string[] _fileName = { "SaveData1", "SaveData2", "SaveData3" };

    private int _currentCheck = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            D.LogWarning("SaveManager의 Instance가 잘못되어 해당 게임오브젝트를 제거했습니다.");
        }

        _gameManager = FindAnyObjectByType<GameManager>();
        _mainManager = FindAnyObjectByType<MainManager>();
        _goldManager = FindAnyObjectByType<GoldManager>();
        _anyEnhanceable = FindAnyObjectByType<Enhanceable>();
        _saveLoadPanel = FindAnyObjectByType<SaveLoadPanel>();
        _path = Application.persistentDataPath + "/";
        D.Log($"세이브 저장 경로 : {_path}", this);

        GetSaveArray();
    }

    void Start()
    { 
        _saveLoadPanel.Renewal();
    }

    public void SaveCheck(int index)
    {
        if (SaveArray[index].isSaved == true)
        {
            _saveLoadPanel.SetSaveCheckPanelActive(true);
            _currentCheck = index;
        }
        else
        {
            Save(index);
        }
    }

    public void Save(int index)
    {
        D.Log($"{index}번째 세이브 저장됨!");

        RenewalCurrentSave(); // 현재 값 가져오기 (isSave = true)
        DuplicateSaveDataValue(SaveArray[index], CurrentSave); // 현재 값을 SaveArray[index]에 저장
        
        string data = JsonUtility.ToJson(SaveArray[index]); // SaveArray[index]를 data에 Json으로 만들어 저장
        File.WriteAllText(_path + _fileName[index], data); // 파일 만들어 저장

        _saveLoadPanel.Renewal(); // 새로 그리기
    }

    public void Load(int index)
    {
        D.Log($"{index}번째 세이브 로드됨!");

        DuplicateSaveDataValue(CurrentSave, SaveArray[index]);
        ReflectCurrentSave();

        _saveLoadPanel.SetSaveLoadPanelActive(false);
        _gameManager.MoveForStart();
    }

    public void OnStartButtonClicked()
    {
        CurrentSave.gold = 1000000;
        CurrentSave.preventer = 0;
        CurrentSave.time = 0;
        CurrentSave.currentSword = (Sword)_anyEnhanceable.data;

        ReflectCurrentSave();

        _gameManager.MoveForStart();
    }

    public void OnSaveCheckContinueButtonClicked()
    {
        Save(_currentCheck);
        _saveLoadPanel.SetSaveCheckPanelActive(false);
    }

    public void GetSaveArray()
    {
        for (int i = 0; i < SaveArray.Length; i++)
        {
            if (!File.Exists(_path + _fileName[i])) // 파일 없으면
            {
                SaveArray[i] = new SaveData(); // isSave = false인 빈 SaveData 생성
                D.Log($"{i}번째 세이브를 찾을 수 없음 / SaveArray[i] : {SaveArray[i]}");
                continue; // 다음 파일로
            }

            string data = File.ReadAllText(_path + _fileName[i]); // 가져오기
            SaveArray[i] = JsonUtility.FromJson<SaveData>(data); // Json을 SaveData로 바꾸기
            D.Log($"{i}번째 세이브를 Array에 저장함 / SaveArray[i] : {SaveArray[i]}");
        }
    }

    public void RenewalCurrentSave()
    {
        CurrentSave.gold = _goldManager.Gold;
        CurrentSave.preventer = _mainManager.Preventer;
        CurrentSave.time = _gameManager.Time;
        CurrentSave.currentSword = (Sword)_anyEnhanceable.data;
        CurrentSave.isMakesMoneyButtonOn = _gameManager.makesMoneyButton.activeSelf;
        CurrentSave.isSaved = true;
    }

    public void ReflectCurrentSave()
    {
        _goldManager.SetGold(CurrentSave.gold);
        _mainManager.Preventer = CurrentSave.preventer;
        _gameManager.Time = CurrentSave.time;
        _mainManager.MoveTo(CurrentSave.currentSword);
        _gameManager.SetMakesMoneyButtonActive(CurrentSave.isMakesMoneyButtonOn);
    }

    public void DuplicateSaveDataValue(SaveData first, SaveData second)
    {
        first.gold = second.gold;
        first.preventer = second.preventer;
        first.time = second.time;
        first.currentSword = second.currentSword;
        first.isMakesMoneyButtonOn = second.isMakesMoneyButtonOn;
        first.isSaved = second.isSaved;
    }
    
    public void OnSaveOpenButtonClicked()
    {
        _saveLoadPanel.isSave = true;
        _saveLoadPanel.SetSaveLoadPanelActive(true);
    }

    public void OnLoadOpenButtonClicked()
    {
        _saveLoadPanel.isSave = false;
        _saveLoadPanel.SetSaveLoadPanelActive(true);
    }

    public void OnReturnButtonClicked()
    {
        _saveLoadPanel.SetSaveLoadPanelActive(false);
    }

}