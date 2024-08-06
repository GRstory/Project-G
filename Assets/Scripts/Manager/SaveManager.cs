using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Overlays;
using UnityEngine;

public class SaveManager : SingletonMonobehavior<SaveManager>
{
    private string _savePath = "save.bin";

    public string SavePath { get { return _savePath; } }

    protected override void Awake()
    {
        base.Awake();
    }

    public void Save(SaveSetting _data, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + filePath, FileMode.Create);

        formatter.Serialize(stream, _data);
        stream.Close();
    }

    public SaveSetting Load(string filePath)
    {
        if (File.Exists(Application.dataPath + filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.dataPath + filePath, FileMode.Open);

            SaveSetting data = formatter.Deserialize(stream) as SaveSetting;

            stream.Close();

            return data;
        }
        else
        {
            SaveSetting saveSetting = new SaveSetting();
            Save(saveSetting, filePath);
            Debug.LogError("Save file not found in" + filePath);
            return null;
        }
    }
}
