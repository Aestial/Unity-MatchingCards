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
    User user;
    float time;
    // Notifier
    readonly Notifier notifier = new Notifier();
    public const string ON_FINISHED = "OnFinished";
    public Puzzle Puzzle
    {
        get { return puzzle; }
        set
        {
            puzzle = value;
            time = 0f;            
            matchController.Set(puzzle);
            SetMoves(puzzle.moves);
            SetTimer();
        }
    }
    void Awake()
    {        
        notifier.Subscribe(CardController.ON_FLIPPED, HandleOnFlipped);
        notifier.Subscribe(MatchController.ON_MATCHED, HandleOnMatched);
        notifier.Subscribe(MatchController.ON_FLIPBACK, HandleOnFlipback);
        notifier.Subscribe(UIController.ON_PAUSE, HandleOnPause);
        notifier.Subscribe(UserController.ON_LOGIN, HandleOnLogin);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    void Update()
    {
        if (puzzle.inProgress)
        {
            time += Time.deltaTime;
            if (time >= 1f)
            {
                time = 0f;
                puzzle.seconds++;
                SetTimer();
            }
        }        
    }    
    private void HandleOnFlipped(object[] args)
    {
        Card card = (Card)args[0];
        matchController.CheckCard(card);        
    }
    private void HandleOnMatched(object[] args)
    {
        int type = (int)args[0];
        SetMoves(++puzzle.moves);
        UpdateMatches(type);        
    }
    private void HandleOnFlipback(object[] args)
    {
        SetMoves(++puzzle.moves);
    }
    private void HandleOnPause(object[] args)
    {
        bool isPaused = (bool)args[0];
        //puzzle.inProgress = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
    private void HandleOnLogin(object[] args)
    {
        user = (User)args[0];
    }
    private void UpdateMatches(int match)
    {
        puzzle.matches.Add(match);
        int count = puzzle.matches.Count;
        if (count >= puzzle.totalMatches)
        {
            // GAME OVER
            puzzle.inProgress = false;                        
            Game game = new Game(user, puzzle);
            notifier.Notify(ON_FINISHED, game);         
        }
    }
    private void SetTimer()
    {
        timeText.text = timePrefix + puzzle.seconds;
        puzzle.score = (puzzle.moves * 5) + puzzle.seconds;
    }    
    private void SetMoves(int moves)
    {
        movesText.text = movesPrefix + moves;
        puzzle.score = (puzzle.moves * 5) + puzzle.seconds;
    }
}