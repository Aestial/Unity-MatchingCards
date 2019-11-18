using System;
using UnityEngine;

public class PairController : MonoBehaviour
{   
    //public CardController one;
    //public CardController two;    
    Pair pair;
    readonly Notifier notifier = new Notifier();
    public const string ON_MATCHED = "OnMatched";
    public const string FLIPBACK = "Flipback";
    public void Set(Pair pair)
    {
        this.pair = pair;
    }
    //public PairController (Pair pair)
    //{
    //    this.pair = pair;
    //}
    public void CheckCard(Card card, Action<int> onMatched)
    {
        switch(pair.count)
        {
            case 0:
                pair.one = card;
                pair.count++;
                break;
            case 1:
                pair.two = card;
                pair.count = 0;
                int match = CheckMatch();
                if (match >= 0)
                    onMatched(match);
                break;
        }
    }
    private int CheckMatch()
    {
        if (pair.one.type == pair.two.type)
        {
            //Matched!!!
            int type = pair.one.type;
            notifier.Notify(ON_MATCHED, type);
            return type;
        }
        // Flipback
        notifier.Notify(FLIPBACK);
        return -1;
    }
    //public void CheckCard(CardController cc, Action<int> onMatched)
    //{
    //    // TODO: Improve: refactor
    //    switch(pair.count)
    //    {
    //        case 0:                
    //            one = cc;
    //            pair.one = cc.Card;
    //            pair.count++;
    //            break;
    //        case 1:
    //            two = cc;
    //            pair.one = cc.Card;
    //            pair.count = 0;
    //            int match = CheckMatch();
    //            if(match >= 0)
    //                onMatched(match);
    //            break;
    //    }
    //}
    //private int CheckMatch()
    //{
    //    if (one.Card.type == two.Card.type)
    //    {
    //        //Matched!!!
    //        notifier.Notify(ON_MATCHED, one.Card.type);
    //        return one.Card.type;
    //    }
    //    // Flipback
    //    notifier.Notify(FLIPBACK);
    //    return -1;
    //}
}