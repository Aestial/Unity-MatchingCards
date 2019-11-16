using System.IO;
using UnityEngine;

public class LeaderboardLoader : MonoBehaviour
{
    // Persistence file
    [SerializeField] string fileName;
    string filePath;
    Leaderboard leaderboard;
    // Notifier
    readonly Notifier notifier = new Notifier();
    public const string ON_LEADERBOARD_LOADED = "OnLeaderboardLoaded";
    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
    }
    void Start()
    {
        LeaderboardController controller = GetComponent<LeaderboardController>();
        leaderboard = Get();
        notifier.Notify(ON_LEADERBOARD_LOADED, leaderboard);
        controller.Leaderboard = leaderboard;
    }
    void OnApplicationQuit()
    {
        Save();
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
        return new Leaderboard();
    }
    private void Save()
    {
        string json = JsonUtility.ToJson(leaderboard);
        File.WriteAllText(filePath, json);
        Debug.Log(json);
    }
}