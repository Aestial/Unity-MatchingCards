using UnityEngine;

public class MatchController : MonoBehaviour
{
    Puzzle puzzle;
    Match match;
    readonly Notifier notifier = new Notifier();
    public const string ON_MATCHED = "OnMatched";
    public const string ON_FLIPBACK = "Flipback";
    public void Set(Puzzle puzzle)
    {
        this.puzzle = puzzle;
        match = puzzle.current;
    }    
    public void CheckCard(Card card)
    {
        // *** Match number
        switch (match.count)
        {
            case 0:
                match.current = card.type;
                match.count++;
                return;            
            case 1:                
                match.matched = card.type == match.current;                
                match.count++;
                return;
            case 2:
                match.matched &= card.type == match.current;
                if (match.matched)
                {
                    Matched();
                    return;
                }
                Flipback();
                return;
        }        
    }
    private void Matched()
    {
        match.count = 0;
        match.matched = false;        
        int index = puzzle.matches.Count;
        notifier.Notify(ON_MATCHED, match.current, index);
    }
    private void Flipback()
    {
        match.count = 0;
        notifier.Notify(ON_FLIPBACK);
    }    
}