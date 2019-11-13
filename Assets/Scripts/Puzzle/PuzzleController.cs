using System.Collections;
using System.Collections.Generic;
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

    public void CheckCard(CardController cc)
    {
        Debug.Log("Checking Card: " + cc.card.type);
        puzzle.moves++;
        scoreText.text = scorePrefix + puzzle.moves;
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
    void Start()
    {
        PuzzleConstructor constructor = GetComponent<PuzzleConstructor>();
        puzzle.totalPairs = pc.pairs;
        puzzle.inProgress = true;        
        StartCoroutine(UpdateTimerCoroutine());    
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
    //void Update()
    //{
    //    puzzle.time += Time.deltaTime;
    //}
}
