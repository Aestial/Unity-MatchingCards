using System.Collections.Generic;
using UnityEngine;

public class PuzzleLoader: Loader<PuzzleLoader>
{
    // Build parameters
    [Range(3, 18)] public int matches;   
    Puzzle puzzle;
    PuzzleController controller;
    public void Restart()
    {
        puzzle = Create();
        notifier.Notify(ON_LOADED, puzzle);
        controller.Puzzle = puzzle;
    }
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
    private Puzzle Create()
    {
        Card[] cards = CreateCards();
        return new Puzzle(cards);
    }
    private Card[] CreateCards()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < matches; i++)
        {
            // *** Match number
            for (int j = 0; j < 3; j++)
            {
                Card newCard = new Card(i);
                cards.Add(newCard);
            }
        }        
        ListExtensions.Shuffle(cards);
        return cards.ToArray();
    }   
}