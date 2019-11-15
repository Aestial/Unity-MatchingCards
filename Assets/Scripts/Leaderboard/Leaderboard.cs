using System;

[Serializable]
public class Leaderboard
{
    public Game[] games;
}

[Serializable]
public class Game
{
    public User user;
    public int time;
    public int score;
}