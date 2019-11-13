using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Pair
{
    public CardController one;
    public CardController two;
    public int count;
    public bool match;

    public void CheckMatch()
    {
        if(one.card.type == two.card.type)
        {
            //Matched!!!
            Debug.Log("Cards Match!!!");
        }
        else
        {
            // Flipback
            Debug.Log("Cards don't match, flipping back.");
            one.Flipback();
            two.Flipback();
        }
    }
}

[Serializable]
public class Puzzle
{
    public bool inProgress;
    public int pairs;
    public int moves;
    public int seconds;
}

public class PuzzleController : MonoBehaviour
{
    //[SerializeField] Text scoreText;
    //[SerializeField] Text timeText;
    //[SerializeField] string scorePrefix;
    //[SerializeField] string timePrefix;

    public Puzzle puzzle = new Puzzle();
    Pair pair = new Pair();
    public void CheckCard(CardController cc)
    {
        Debug.Log("Checking Card: " + cc.card.type);
        puzzle.moves++;
        //scoreText.text = scorePrefix + puzzle.moves;
        switch(pair.count)
        {
            case 0:
                pair.one = cc;
                pair.count++;
                break;
            case 1:
                pair.two = cc;
                pair.count = 0;
                pair.CheckMatch();
                break;
        }
    }    
    void Start()
    {
        PuzzleCreator creator = GetComponent<PuzzleCreator>();
        puzzle.pairs = creator.pairs;
        puzzle.inProgress = true;        
        StartCoroutine(UpdateTimerCoroutine());
        //InvokeRepeating("UpdateTimer", 0f, 1f);
    }
    private IEnumerator UpdateTimerCoroutine()
    {
        while (puzzle.inProgress)
        {
            yield return new WaitForSecondsRealtime(1f);
            puzzle.seconds++;
            //timeText.text = timePrefix + ++puzzle.seconds;
        }
    }
    //void Update()
    //{
    //    puzzle.time += Time.deltaTime;
    //}
    //private void UpdateTimer()
    //{        
    //    timeText.text = timePrefix + ++puzzle.time;
    //}
}
