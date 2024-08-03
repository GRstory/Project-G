using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : SingletonMonobehavior<DataManager>
{
    private string _settings = "Setting.json";

    private SaveSetting _saveSetting = new SaveSetting();

    public SaveSetting SaveSetting { get { return _saveSetting; } }

    protected override void Awake()
    {
        base.Awake();

    }

    public void LoadSettingData()
    {
        string filePath = Application.persistentDataPath + "/" + _settings;
        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            _saveSetting = JsonUtility.FromJson<SaveSetting>(fromJsonData);
        }
    }

    public void SaveSettingData()
    {
        string filePath = Application.persistentDataPath + "/" + _settings;
        string toJsonData = JsonUtility.ToJson(_saveSetting, true);

        File.WriteAllText(filePath, toJsonData);
    }
}
