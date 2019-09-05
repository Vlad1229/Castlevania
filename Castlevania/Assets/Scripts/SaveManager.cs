using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;

public static class SaveManager
{
    public static void Save(string path, LevelData save)
    {
        BinaryFormatter formater = new BinaryFormatter();
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            formater.Serialize(fs, save);
        }
    }

    public static LevelData Read(string path)
    {
        LevelData levelData;
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                levelData = (LevelData)formater.Deserialize(fs);
            }
        }
        else
        {
            levelData = new LevelData();
        }
        return levelData;
    }    
}

