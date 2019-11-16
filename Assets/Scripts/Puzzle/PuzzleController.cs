using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{    
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] string scorePrefix;
    [SerializeField] string timePrefix;
    [SerializeField] Puzzle puzzle;
    readonly Pair pair = new Pair();
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
            puzzle.inProgress = true;            
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
        CardController cardController = (CardController)args[0];
        CheckCard(cardController);
        puzzle.moves++;
        scoreText.text = scorePrefix + puzzle.moves;
    }
    private void CheckCard(CardController cc)
    {
        switch (pair.count)
        {
            case 0:
                pair.one = cc;
                pair.count++;
                break;
            case 1:
                pair.two = cc;
                pair.count = 0;
                int match = pair.CheckMatch();
                if (match >= 0)
                    UpdateMatches(match);
                break;
            default:
                pair.one = pair.two = null;
                break;
        }
    }
    private void UpdateMatches(int match)
    {
        puzzle.matches.Add(match);
        int score = puzzle.matches.Count;        
        if (score >= puzzle.pairs)
        {
            puzzle.inProgress = false;
            int time = puzzle.seconds;
            User user = new User();
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
}
public class Pair
{
    public CardController one;
    public CardController two;
    public int count;
    readonly Notifier notifier = new Notifier();
    public const string ON_MATCHED = "OnMatched";
    public int CheckMatch()
    {
        if (one.Card.type == two.Card.type)
        {
            //Matched!!!
            notifier.Notify(ON_MATCHED, one.Card.type);
            return one.Card.type;
        }
        // Flipback
        one.Flipback();
        two.Flipback();
        return -1;
    }
}