using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    private static SaveSystem _instance;

    public static SaveSystem Instance
    {
        get
        {
            if(_instance == null)
                _instance = new SaveSystem();

            return _instance;
        }
    }

    private SaveSystem()
    {
        
    }

    public void Save(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public GameData Load()
    {
        string path = Application.persistentDataPath + "/saves.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Path error, no save date");
            return null;
        }
    }
}
