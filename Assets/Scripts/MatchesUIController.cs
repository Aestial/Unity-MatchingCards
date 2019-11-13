using System.Collections;
using UnityEngine;

public class MatchesUIController : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;
    [SerializeField] PuzzleCreator creator;
    readonly Notifier notifier = new Notifier();

    public void AddMatch(int type)
    {
        GameObject newMatch = Instantiate(prefab, container);
        CardImage cardImage = newMatch.GetComponent<CardImage>();
        cardImage.SetImage(creator.sprites[type].unlocked);
    }
    void Start()
    {
        notifier.Subscribe(Pair.ON_MATCHED, HandleOnMatched);
        //StartCoroutine(TestCoroutine());
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnMatched(params object[] args)
    {
        int type = (int)args[0];
        AddMatch(type);
    }
    //private IEnumerator TestCoroutine()
    //{
    //    yield return new WaitForSeconds(1f);
    //    AddMatch(5);
    //}
}