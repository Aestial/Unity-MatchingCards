using System;

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