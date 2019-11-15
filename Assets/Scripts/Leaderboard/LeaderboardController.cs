using System.IO;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    // Persistence file
    [SerializeField] string fileName;
    string filePath;
    Leaderboard leaderboard;
    // Notifier
    readonly Notifier notifier = new Notifier();
    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
    }
    void Start()
    {
        leaderboard = Get();
        //notifier.Notify(ON_LOADED, puzzle);        
    }
    private Leaderboard Get()
    {
        return File.Exists(filePath) ? Load() : Create();
    }
    private Leaderboard Load()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Leaderboard>(json);
    }
    private Leaderboard Create()
    {
        Card[] cards = CreateCards();
        puzzle = new Puzzle(cards);
        return puzzle;
    }
}
