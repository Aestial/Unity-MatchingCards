using System.Collections;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Card card = new Card();
    public Sprite visible;
    public Sprite disabled;
    [SerializeField] Sprite invisible;
    [SerializeField] float showTime;
    SpriteRenderer sr;
    Notifier notifier = new Notifier();
    public const string ON_FLIPPED = "OnFlipped";   

    public CardState State
    {
        get { return card.state; }
        set
        {
            card.state = value;
            Draw(value);
        }
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        State = CardState.Invisible;
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
}