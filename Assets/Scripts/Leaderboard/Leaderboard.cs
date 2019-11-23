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
    public Puzzle puzzle;
    public Game(User user, Puzzle puzzle)
    {
        this.user = user;        
        this.puzzle = puzzle;
    }
}