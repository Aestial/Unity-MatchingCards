using System;
using System.IO;
using UnityEngine;

public abstract class Loader<T> : MonoBehaviour where T : MonoBehaviour
{    
    [SerializeField] protected string fileName;
    string filePath;
    // Notifier
    protected Notifier notifier = new Notifier();
    public readonly static string ON_LOADED = "OnLoaded" + typeof(T).Name;
    protected void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
    }
    protected void SetFilePath(string fileName)
    {
        this.fileName = fileName;
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
    }    
    protected void Save(object obj)
    {
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(filePath, json);
        Debug.Log(json);
    }
    protected U Get<U>(Func<U> createFunction)
    {        
        return File.Exists(filePath) ? Load<U>() : createFunction();
    }
    protected U Load<U>()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<U>(json);
    }
}