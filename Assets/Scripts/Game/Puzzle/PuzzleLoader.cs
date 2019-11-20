using System.Collections.Generic;
using UnityEngine;

public class PuzzleLoader: Loader<PuzzleLoader>
{
    // Build parameters
    [Range(2, 35)] public int pairs;   
    Puzzle puzzle;
    PuzzleController controller;    
    void Start()
    {
        controller = GetComponent<PuzzleController>();
        puzzle = Get(Create);
        notifier.Notify(ON_LOADED, puzzle);  
        controller.Puzzle = puzzle;
    }
    void OnApplicationQuit()
    {
        Save(puzzle);
    }
    public void Restart()
    {
        puzzle = Create();
        notifier.Notify(ON_RESTART, puzzle);
        controller.Puzzle = puzzle;
    }    
    private Puzzle Create()
    {
        Card[] cards = CreateCards();
        return new Puzzle(cards);
    }
    private Card[] CreateCards()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < pairs; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Card newCard = new Card(i);
                cards.Add(newCard);
            }
        }        
        ListExtensions.Shuffle(cards);
        return cards.ToArray();
    }   
}