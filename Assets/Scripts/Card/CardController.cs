using System.Collections;
using UnityEngine;

public class CardController : MonoBehaviour
{    
    public Sprite visible;
    public Sprite disabled;
    [SerializeField] Sprite invisible;
    [SerializeField] float showTime;
    Card card;
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
    }
    void OnMouseUp()
    {
        if(State == CardState.Invisible)
        {
            State = CardState.Visible;
            notifier.Notify(ON_FLIPPED, this);
        }
    }
    public void Flipback()
    {
        StartCoroutine(FlipbackCoroutine());
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