using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{    
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] string scorePrefix;
    [SerializeField] string timePrefix;
    readonly Pair pair = new Pair();
    readonly Notifier notifier = new Notifier();
    [SerializeField] Puzzle puzzle;
    public Puzzle Puzzle
    {
        get { return puzzle; }
        set
        {
            puzzle = value;
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
    private void HandleOnFlipped(params object[] args)
    {
        CardController cardController = (CardController)args[0];
        CheckCard(cardController);
        puzzle.moves++;
        scoreText.text = scorePrefix + puzzle.moves;
    }
    private void CheckCard(CardController cc)
    {
        Debug.Log("Checking Card: " + cc.Card.type);
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
        if (puzzle.matches.Count >= puzzle.pairs)
        {
            puzzle.inProgress = false;
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
