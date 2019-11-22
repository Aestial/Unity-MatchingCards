using System;
using System.Collections.Generic;

[Serializable]
public class Leaderboard
{    
    public List<Game> games;
    public Leaderboard ()
    {
        games = new List<Game>();
    }
}

[Serializable]
public class Game
{
    public User user;
    public int time;
    public int score;
    public Game(User user, int score, int time)
    {
        this.user = user;        
        this.score = score;
        this.time = time;
    }
}