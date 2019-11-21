using System;
using UnityEngine;

public class PairController : MonoBehaviour
{
    //[SerializeField] int matchCount;
    Pair pair;    
    readonly Notifier notifier = new Notifier();
    public const string ON_MATCHED = "OnMatched";
    public const string FLIPBACK = "Flipback";
    public void Set(Pair pair)
    {
        this.pair = pair;
    }    
    public void CheckCard(Card card, Action<int> onMatched)
    {
        // *** Match number
        switch (pair.count)
        {
            case 0:
                pair.current = card.type;
                pair.count++;
                return;            
            case 1:
                if (card.type != pair.current)
                {
                    Flipback();
                    return;
                }
                pair.count++;
                return;
            case 2:
                if (card.type != pair.current)
                {
                    Flipback();
                    return;
                }                
                pair.count = 0;
                notifier.Notify(ON_MATCHED, pair.current);
                onMatched(pair.current);
                return;
        }        
    }
    private void Flipback()
    {
        // Flipback
        pair.count = 0;
        notifier.Notify(FLIPBACK);
    }    
}