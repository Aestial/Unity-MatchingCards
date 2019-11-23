using System.Collections;
using UnityEngine;

public class CardController : MonoBehaviour
{       
    [SerializeField] Sprite invisible;
    [SerializeField] float showTime;
    [SerializeField] Card card;
    [HideInInspector] public Sprite visible;
    [HideInInspector] public Sprite disabled;
    SpriteRenderer sr;
    // Notifier
    readonly Notifier notifier = new Notifier();
    public const string ON_FLIPPED = "OnFlipped";
    public Card Card
    {
        get { return card; }
        set
        {
            card = value;
            SetSprites(card.type);
            State = card.state;
        }
    }
    public CardState State
    {
        get { return card.state; }
        set
        {
            card.state = value;
            Draw(value);
        }
    }
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
    void OnMouseUp()
    {
        if(State == CardState.Invisible)
        {
            State = CardState.Visible;
            notifier.Notify(ON_FLIPPED, Card);
        }
    }
    private IEnumerator FlipbackCoroutine()
    {
        yield return new WaitForSeconds(showTime);
        State = CardState.Invisible;        
    }
    private void Draw(CardState state)
    {
        switch (state)
        {
            case CardState.Invisible:
                sr.sprite = invisible;
                break;
            case CardState.Visible:
            case CardState.Matched:
                sr.sprite = visible;
                break;
            case CardState.Disabled:
                sr.sprite = disabled;
                break;
        }
    }
    private void SetSprites(int type)
    {
        visible = CardSprites.Instance.sprites[type].unlocked;
        disabled = CardSprites.Instance.sprites[type].locked;
    }
}