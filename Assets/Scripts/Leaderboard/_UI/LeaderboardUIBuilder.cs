using System;
using UnityEngine;

public class LeaderboardUIBuilder : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;    
    // Notifier
    readonly Notifier notifier = new Notifier();
    void Awake()
    {        
        notifier.Subscribe(LeaderboardLoader.ON_LEADERBOARD_LOADED, HandleOnLoaded);
        notifier.Subscribe(PuzzleController.ON_FINISHED, HandleOnFinished);
    }
    private void HandleOnLoaded(object[] args)
    {
        Leaderboard leaderboard = (Leaderboard)args[0];
        ShowGames(leaderboard.games.ToArray());
    }
    private void HandleOnFinished(object[] args)
    {
        Game game = (Game)args[0];
        AddGame(game);        
    }
    private void ShowGames(Game[] games)
    {   for (int i = 0; i < games.Length; i++)
        {
            AddGame(games[i]);
        }
    }
    private void AddGame(Game game)
    {
        GameObject newGame = Instantiate(prefab, container);
        GameUI gameUI = newGame.GetComponent<GameUI>();
        gameUI.Set(game);
    }    
}
