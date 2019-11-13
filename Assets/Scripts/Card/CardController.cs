using System.Collections;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Card card = new Card();    
    public Sprite visible;
    public Sprite disabled;
    [SerializeField] Sprite invisible;
    [SerializeField] float showTime;
    PuzzleController puzzle;
    SpriteRenderer sr;
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
        puzzle = FindObjectOfType<PuzzleController>();
        sr = GetComponent<SpriteRenderer>();
        State = CardState.Invisible;
    }
    void OnMouseUp()
    {
        if(State == CardState.Invisible)
        {
            State = CardState.Visible;
            puzzle.CheckCard(this);
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
