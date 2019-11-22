using System;
using UnityEngine;

public class MatchesUI : MonoBehaviour
{
    [SerializeField] GameObject matchPrefab;
    [SerializeField] GameObject unmatchPrefab;
    [SerializeField] Transform container;
    readonly Notifier notifier = new Notifier();
    void Awake()
    {        
        notifier.Subscribe(PuzzleLoader.ON_LOADED, HandleOnLoaded);
        //notifier.Subscribe(PuzzleLoader.ON_RESTART, HandleOnRestart);
        notifier.Subscribe(MatchController.ON_MATCHED, HandleOnMatched);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnLoaded(object[] args)
    {
        Puzzle puzzle = (Puzzle)args[0];
        DeleteMatches();
        ShowMatches(puzzle);
    }
    //private void HandleOnRestart(object[] args)
    //{
    //    DeleteMatches();
    //}
    private void HandleOnMatched(object[] args)
    {
        int type = (int)args[0];
        int index = (int)args[1];
        DeleteUnmatch();
        AddMatch(type, index);
    }    
    private void ShowMatches(Puzzle puzzle)
    {
        for (int i = 0; i < puzzle.totalMatches; i++)
        {
            if(i < puzzle.matches.Count)            
                AddMatch(puzzle.matches[i], i, false);
            else
                AddUnmatch();
        }
    }
    private void AddUnmatch()
    {
        Instantiate(unmatchPrefab, container);
    }
    private void AddMatch(int type, int index, bool delete = true)
    {
        if (delete) DeleteUnmatch();
        GameObject newMatch = Instantiate(matchPrefab, container);
        newMatch.transform.SetSiblingIndex(index);
        CardUI cardUI = newMatch.GetComponent<CardUI>();
        cardUI.Set(CardSprites.Instance.sprites[type].unlocked);        
    }
    private void DeleteUnmatch()
    {
        Destroy(container.GetChild(container.childCount - 1).gameObject);
    }
    private void DeleteMatches()
    {
        if(container.childCount > 0)
        {
            for (int i = 0; i < container.childCount; i++)
                Destroy(container.GetChild(i).gameObject);
        }        
    }
}