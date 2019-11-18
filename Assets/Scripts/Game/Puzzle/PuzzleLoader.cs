using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleLoader: MonoBehaviour
{
    // Persistence file
    [SerializeField] string fileName;
    string filePath;
    // Build parameters
    [Range(2, 35)] public int pairs;   
    Puzzle puzzle;
    PuzzleController controller;
    // Notifier
    readonly Notifier notifier = new Notifier();
    public const string ON_LOADED = "OnLoaded";
    public const string ON_RESTART = "OnRestart";

    void Awake()
    {        
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        controller = GetComponent<PuzzleController>();
    }
    void Start()
    {        
        puzzle = Get();        
        notifier.Notify(ON_LOADED, puzzle);  
        controller.Puzzle = puzzle;
    }
    void OnApplicationQuit()
    {
        Save();
    }
    public void Restart()
    {
        puzzle = Create();
        notifier.Notify(ON_RESTART, puzzle);
        controller.Puzzle = puzzle;
    }
    private void Save()
    {
        string json = JsonUtility.ToJson(puzzle);
        File.WriteAllText(filePath, json);
        Debug.Log(json);
    }
    private Puzzle Get()
    {        
        return File.Exists(filePath) ? Load() : Create();
    }
    private Puzzle Load()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Puzzle>(json);       
    }
    private Puzzle Create()
    {
        Card[] cards = CreateCards();
        return new Puzzle(cards);        
    }
    private Card[] CreateCards()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < pairs; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Card newCard = new Card(i);
                cards.Add(newCard);
            }
        }        
        ListExtensions.Shuffle(cards);
        return cards.ToArray();
    }   
}