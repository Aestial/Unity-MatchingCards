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
    new void Awake()
    {
        controller = GetComponent<PuzzleController>();
        notifier.Subscribe(UserController.ON_LOGIN, HandleOnLogin);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnLogin(object[] args)
    {
        User user = (User)args[0];
        SetFilePath(user.filename);        
        puzzle = Get(Create);
        if (!puzzle.inProgress) puzzle = Create();
        controller.Puzzle = puzzle;
        notifier.Notify(ON_LOADED, puzzle);
    }
    void OnApplicationQuit()
    {
        if (puzzle != null)
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