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
public class Pair
{
    public CardController one;
    public CardController two;
    public int count;
    readonly Notifier notifier = new Notifier();
    public const string ON_MATCHED = "OnMatched";
    public int CheckMatch()
    {
        if (one.Card.type == two.Card.type)
        {
            //Matched!!!
            notifier.Notify(ON_MATCHED, one.Card.type);
            return one.Card.type;
        }
        // Flipback
        one.Flipback();
        two.Flipback();
        return -1;
    }
}