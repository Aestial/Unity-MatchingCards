public class LeaderboardLoader : Loader<LeaderboardLoader>
{
    Leaderboard leaderboard;
    LeaderboardController controller;
    void Start()
    {
        controller = GetComponent<LeaderboardController>();
        leaderboard = Get(Create);
        notifier.Notify(ON_LOADED, leaderboard);
        controller.Leaderboard = leaderboard;
    }
    void OnApplicationQuit()
    {
        Save(leaderboard);
    }
    private Leaderboard Create()
    {
        return new Leaderboard();
    }
}