using System.Collections.Generic;
using UnityEngine;

public class PuzzleUIBuilder : MonoBehaviour
{
    // Card GameObjects
    [SerializeField] GameObject cardPrefab;
    // Placing parameters    
    [SerializeField] Vector2 offset;
    [SerializeField] Vector2 spacing;
    [SerializeField] int columns;
    // Notifier
    readonly Notifier notifier = new Notifier();
    void Awake()
    {
        notifier.Subscribe(PuzzleLoader.ON_LOADED, HandleOnLoaded);
        //notifier.Subscribe(PuzzleLoader.ON_RESTART, HandleOnRestart);
        notifier.Subscribe(PuzzleController.ON_FINISHED, HandleOnFinished);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnLoaded(object[] args)
    {
        Puzzle puzzle = (Puzzle)args[0];
        Create(puzzle);
    }
    //private void HandleOnRestart(object[] args)
    //{
    //    Puzzle puzzle = (Puzzle)args[0];
    //    Create(puzzle);
    //}
    private void HandleOnFinished(object[] args)
    {
        DeleteCards();
        ResetPlacement();
    }
    private void Create(Puzzle puzzle)
    {
        GameObject[] cardGOs = CreateGOs(puzzle.cards);
        PlaceCards(cardGOs);
    }
    private void DeleteCards()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject); 
        }        
    }
    private GameObject[] CreateGOs(Card[] cards)
    {
        List<GameObject> GOs = new List<GameObject>();
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, transform);
            CardController cc = newCard.GetComponent<CardController>();
            cc.Card = cards[i];
            GOs.Add(newCard);
        }
        return GOs.ToArray();
    }
    private void PlaceCards(GameObject[] gameObjects)
    {
        int length = gameObjects.Length;
        for (int i = 0; i < length; i++)
        {
            Vector3 position = new Vector3(i % columns, i / columns);
            position *= spacing;
            gameObjects[i].transform.localPosition = position;
        }
        Vector3 centerOffset = new Vector3((columns - 1) / 2.0f, (length - 1) / columns / 2.0f);
        centerOffset *= spacing;
        transform.position -= centerOffset;
        transform.position += new Vector3(offset.x, offset.y);
    }
    private void ResetPlacement()
    {
        transform.position = Vector3.zero;
    }
}
