using UnityEngine;

public class LeaderboardUIBuilder : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;
    Leaderboard leaderboard;
    // Notifier
    readonly Notifier notifier = new Notifier();
    void Awake()
    {        
        notifier.Subscribe(LeaderboardLoader.ON_LOADED, HandleOnLoaded);
        notifier.Subscribe(PuzzleController.ON_FINISHED, HandleOnFinished);
    }
    private void HandleOnLoaded(object[] args)
    {
        leaderboard = (Leaderboard)args[0];
        ShowGames(leaderboard.games.ToArray());
    }
    private void HandleOnFinished(object[] args)
    {
        //Game game = (Game)args[0];
        // Sort and show all        
        ShowGames(leaderboard.games.ToArray());
    }
    private void ShowGames(Game[] games)
    {
        DeleteAll();
        for (int i = 0; i < games.Length; i++)
        {
            AddGame(games[i], i + 1);
        }
    }
    private void DeleteAll()
    {
        if (container.childCount < 0) return;
        for (int i = 0; i < container.childCount; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
    private void AddGame(Game game, int index)
    {
        GameObject newGame = Instantiate(prefab, container);
        GameUI gameUI = newGame.GetComponent<GameUI>();
        gameUI.Set(game, index);
    }    
}
