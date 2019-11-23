using System;

public enum CardState
{
    Invisible,
    Visible,
    Matched,
    Disabled
}
[Serializable]
public class Card
{
    public int type;
    public CardState state;
    public Card(int type, CardState state = CardState.Invisible)
    {
        this.type = type;
        this.state = state;
    }
}