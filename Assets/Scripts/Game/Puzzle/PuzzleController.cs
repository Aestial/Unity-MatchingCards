using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{    
    [SerializeField] Text movesText;
    [SerializeField] Text timeText;
    [SerializeField] string movesPrefix = "Moves";
    [SerializeField] string timePrefix;
    [SerializeField] MatchController matchController;    
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
            matchController.Set(puzzle.current);
            SetMoves(puzzle.moves);
            StopAllCoroutines();
            StartCoroutine(UpdateTimerCoroutine());
        }
    }
    void Awake()
    {        
        notifier.Subscribe(CardController.ON_FLIPPED, HandleOnFlipped);
        notifier.Subscribe(MatchController.ON_MATCHED, HandleOnMatched);
        notifier.Subscribe(MatchController.ON_FLIPBACK, HandleOnFlipback);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();        
    }    
    private void HandleOnFlipped(object[] args)
    {
        Card card = (Card)args[0];
        matchController.CheckCard(card);        
    }
    private void HandleOnMatched(object[] args)
    {
        int type = (int)args[0];
        UpdateMatches(type);        
    }
    private void HandleOnFlipback(object[] args)
    {
        SetMoves(++puzzle.moves);
    }   
    private void UpdateMatches(int match)
    {
        puzzle.matches.Add(match);
        int count = puzzle.matches.Count;
        if (count >= puzzle.totalMatches)
        {
            // TODO GAME OVER
            puzzle.inProgress = false;
            // TODO: FIX this
            int score = puzzle.score;
            int time = puzzle.seconds;            
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
            timeText.text = timePrefix + puzzle.seconds++;
            puzzle.score = (puzzle.moves * 5) + puzzle.seconds;
        }
    }
    private void SetMoves(int score)
    {
        movesText.text = movesPrefix + score;
    }
}