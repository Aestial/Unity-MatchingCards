using UnityEngine;

public class MatchesUI : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;
    readonly Notifier notifier = new Notifier();
    void Awake()
    {
        notifier.Subscribe(PairController.ON_MATCHED, HandleOnMatched);
        notifier.Subscribe(PuzzleLoader.ON_LOADED, HandleOnLoaded);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnLoaded(object[] args)
    {
        Puzzle puzzle = (Puzzle)args[0];
        ShowMatches(puzzle.matches.ToArray());
    }
    private void HandleOnMatched(object[] args)
    {
        int type = (int)args[0];
        AddMatch(type);
    }
    private void ShowMatches(int[] matches)
    {
        for (int i = 0; i < matches.Length; i++)
        {
            AddMatch(matches[i]);
        }
    }
    private void AddMatch(int type)
    {
        GameObject newMatch = Instantiate(prefab, container);
        CardUI cardUI = newMatch.GetComponent<CardUI>();
        cardUI.Set(CardSprites.Instance.sprites[type].unlocked);
    }
}