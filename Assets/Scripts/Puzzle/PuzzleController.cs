using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] string scorePrefix;
    [SerializeField] string timePrefix;
    public Puzzle puzzle = new Puzzle();
    readonly Pair pair = new Pair();
    Notifier notifier = new Notifier();
    
    void Start()
    {
        notifier.Subscribe(CardController.ON_FLIPPED, HandleOnFlipped);
        StartCoroutine(UpdateTimerCoroutine());        
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
        Debug.Log("Checking Card: " + cc.card.type);        
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
        if (puzzle.matches.Count >= puzzle.totalPairs)
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
