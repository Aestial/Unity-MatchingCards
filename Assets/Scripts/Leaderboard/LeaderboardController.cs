using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{    
    [SerializeField] Leaderboard leaderboard;    
    // Notifier
    readonly Notifier notifier = new Notifier();
    public Leaderboard Leaderboard
    {
        get { return leaderboard; }
        set
        {
            leaderboard = value;
            //gameList = new List<Game>(leaderboard.games);
        }
    }
    void Awake()
    {
        notifier.Subscribe(PuzzleController.ON_FINISHED, HandleOnFinished);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();    
    }
    private void HandleOnFinished(object[] args)
    {
        Game game = (Game)args[0];
        AddGame(game);
    }
    private void AddGame(Game game)
    {
        leaderboard.games.Add(game);        
    }        
}
