using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardState
{
    Invisible,
    Visible,
    Disabled
}

[Serializable]
public class Card
{
    public int type;
    public CardState state;
    //public int pairId;
}

public class CardController : MonoBehaviour
{
    public Card card = new Card();
    public Sprite invisible;
    public Sprite visible;
    public Sprite disabled;
    [SerializeField] float showTime;
    new SpriteRenderer renderer;
    PuzzleController puzzle;
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
        renderer = GetComponent<SpriteRenderer>();
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
                renderer.sprite = invisible;
                break;
            case CardState.Visible:
                renderer.sprite = visible;
                break;
            case CardState.Disabled:
                renderer.sprite = disabled;
                break;
        }
    }
}
