using System;
using System.Collections.Generic;

[Serializable]
public class Puzzle
{   
    public Card[] cards;
    public bool inProgress;
    public int pairs;
    public int moves;
    public int seconds;
    public List<int> matches;
    public Puzzle (Card[] cards)
    {
        this.cards = cards;
        pairs = cards.Length / 2;
        matches = new List<int>();
    }
}