using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileWork : MonoBehaviour
{
    public string StateDataPath = @"/GameState.dat";
    private Model m;
    void Start()
    {
        m = GetComponent<Model>();
        LoadState();
    }


    void Update()
    {

    }
   public void SaveState()
    {
        if (m.GameEnded)
        {
            m.State = new int[3, 3];
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + StateDataPath);
        bf.Serialize(file, m.State);
        file.Close();
    }
    public void LoadState()
    {
        if (File.Exists(Application.persistentDataPath + StateDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + StateDataPath, FileMode.Open);
            m.State = (int[,])bf.Deserialize(file);
            file.Close();
            m.ui.ShowMarks();
            Debug.Log("Game data loaded");
        }
    }

}
