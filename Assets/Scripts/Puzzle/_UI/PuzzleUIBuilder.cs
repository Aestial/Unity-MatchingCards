using System.Collections.Generic;
using UnityEngine;

public class PuzzleUIBuilder : MonoBehaviour
{
    // Card GameObjects
    [SerializeField] GameObject cardPrefab;
    // Placing parameters
    [SerializeField] int columns;
    [SerializeField] Vector2 spacing;
    // Notifier
    readonly Notifier notifier = new Notifier();
    void Awake()
    {
        notifier.Subscribe(PuzzleLoader.ON_LOADED, HandleOnLoaded);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnLoaded(object[] args)
    {
        Puzzle puzzle = (Puzzle)args[0];
        GameObject[] cardGOs = CreateCardsGO(puzzle.cards);
        PlaceCards(cardGOs);        
    }
    private GameObject[] CreateCardsGO(Card[] cards)
    {
        List<GameObject> cardGOList = new List<GameObject>();
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, transform);
            CardController cc = newCard.GetComponent<CardController>();
            cc.Card = cards[i];
            cardGOList.Add(newCard);
        }
        return cardGOList.ToArray();
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
        Vector3 offset = new Vector3((columns - 1) / 2.0f, (length - 1) / columns / 2.0f);
        offset *= spacing;
        transform.position -= offset;
    }    
}
