﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{    
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] string scorePrefix;
    [SerializeField] string timePrefix;
    [SerializeField] PairController pairController;
    // SerializeField for watching on Inspector
    [SerializeField] Puzzle puzzle;    
    // Notifier
    readonly Notifier notifier = new Notifier();
    public const string ON_FINISHED = "OnFinished";
    public Puzzle Puzzle
    {
        get { return puzzle; }
        set
        {
            puzzle = value;
            // TODO: Validate            
            pairController.Set(puzzle.current);
            SetScore(puzzle.moves);
            StopAllCoroutines();
            StartCoroutine(UpdateTimerCoroutine());
        }
    }
    void Awake()
    {
        notifier.Subscribe(CardController.ON_FLIPPED, HandleOnFlipped);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();        
    }    
    private void HandleOnFlipped(object[] args)
    {
        Card card = (Card)args[0];
        pairController.CheckCard(card, UpdateMatches);
        SetScore(++puzzle.moves);
    }    
    private void UpdateMatches(int match)
    {
        puzzle.matches.Add(match);
        int count = puzzle.matches.Count;
        if (count >= puzzle.pairs)
        {
            puzzle.inProgress = false;
            int score = puzzle.moves;
            int time = puzzle.seconds;
            // TODO: Check this
            User user = new User("Player Name");
            Game game = new Game(user, score, time);
            notifier.Notify(ON_FINISHED, game);
        }
    }
    private IEnumerator UpdateTimerCoroutine()
    {
        while (puzzle.inProgress)
        {
            yield return new WaitForSecondsRealtime(1f);
            timeText.text = timePrefix + ++puzzle.seconds;
        }
    }
    private void SetScore(int score)
    {
        scoreText.text = scorePrefix + score;
    }
}