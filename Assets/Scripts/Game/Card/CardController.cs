using System.Collections;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] Card card;
    [SerializeField] CardDisplay display;
    [SerializeField] float showTime;
    // Notifier
    readonly Notifier notifier = new Notifier();
    public const string ON_FLIPPED = "OnFlipped";
    public Card Card
    {
        get { return card; }
        set
        {
            card = value;
            display.GetAsset(card.type);
            State = card.state;
        }
    }
    public CardState State
    {
        get { return card.state; }
        set
        {
            card.state = value;
            display.Turn(value);
        }
    }
    void OnMouseUp()
    {
        if (State == CardState.Invisible)
        {
            State = CardState.Visible;
            notifier.Notify(ON_FLIPPED, Card);
        }
    }
    void Awake()
    {
        notifier.Subscribe(MatchController.ON_MATCHED, HandleOnMatched);
        notifier.Subscribe(MatchController.ON_FLIPBACK, HandleFlipBack);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnMatched(object[] args)
    {
        int type = (int)args[0];
        if (card.type == type)
        {
            State = CardState.Matched;
        }
    }    
    private void HandleFlipBack(object[] args)
    {        
        if (State == CardState.Visible)
        {
            StartCoroutine(FlipbackCoroutine());
        }
    }    
    private IEnumerator FlipbackCoroutine()
    {
        yield return new WaitForSeconds(showTime);
        State = CardState.Invisible;        
    }
}