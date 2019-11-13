using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pair
{
    public CardController one;
    public CardController two;
    public int count;
    public bool match;

    public void CheckMatch()
    {
        if(one.card.type == two.card.type)
        {
            //Matched!!!
            Debug.Log("Cards Match!!!");
        }
        else
        {
            // Flipback
            Debug.Log("Cards don't match, flipping back.");
            one.Flipback();
            two.Flipback();
        }
    }
}

[Serializable]
public class Puzzle
{
    public bool inProgress;
    public int moves;
    public float time;
}

public class PuzzleController : MonoBehaviour
{
    Puzzle puzzle = new Puzzle();
    Pair pair = new Pair();
    public void CheckCard(CardController cc)
    {
        Debug.Log("Checking Card: " + cc.card.type);
        puzzle.moves++;
        switch(pair.count)
        {
            case 0:
                pair.one = cc;
                pair.count++;
                break;
            case 1:
                pair.two = cc;
                pair.count = 0;
                pair.CheckMatch();
                break;
        }        
    }
    void Start()
    {
        puzzle.inProgress = true;    
    }
    void Update()
    {
        puzzle.time += Time.deltaTime;        
    }    
}
