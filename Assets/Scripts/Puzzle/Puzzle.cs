using System;
using System.Collections.Generic;

[Serializable]
public class Puzzle
{
    public bool inProgress;
    public List<int> matches;
    public int totalPairs;
    public int moves;
    public int seconds;
}

//[Serializable]
public class Pair
{
    public CardController one;
    public CardController two;
    public int count;
    public bool match;
    Notifier notifier = new Notifier();
    public const string ON_MATCHED = "OnMatched";
    public int CheckMatch()
    {
        if (one.card.type == two.card.type)
        {
            //Matched!!!
            notifier.Notify(ON_MATCHED, one.card.type);
            return one.card.type;
        }
        // Flipback
        one.Flipback();
        two.Flipback();
        return -1;
    }
}