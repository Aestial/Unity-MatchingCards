using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PuzzleConstructor: MonoBehaviour
{
    // Parameters
    [Range(5, 35)] public int pairs;
    // Card GameObjects
    [SerializeField] GameObject cardPrefab;
    // Placing parameters
    [SerializeField] int columns;
    [SerializeField] Vector2 spacing;
    // Assets
    [SerializeField] string fileName;
    string filePath;
    Puzzle puzzle;
    PuzzleController controller;
    void Awake()
    {
        controller = GetComponent<PuzzleController>();
        filePath = Path.Combine(Application.persistentDataPath, fileName);
    }
    void Start()
    {        
        puzzle = Get();
        GameObject[] cardGOs = CreateCardsGO(puzzle.cards);
        PlaceCards(cardGOs);
        controller.Puzzle = puzzle;
    }
    void OnApplicationQuit()
    {
        Save();
    }    
    private void Save()
    {
        Debug.Log(filePath);
        string json = JsonUtility.ToJson(puzzle);
        File.WriteAllText(filePath, json);
        Debug.Log(json);
    }
    private Puzzle Get()
    {        
        return File.Exists(filePath) ? Load() : Create();
    }
    private Puzzle Load()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Puzzle>(json);       
    }
    private Puzzle Create()
    {
        Card[] cards = CreateCards();
        puzzle = new Puzzle(cards);
        return puzzle;
    }
    private Card[] CreateCards()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < pairs; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Card newCard = new Card(i);
                cards.Add(newCard);
            }
        }        
        ListExtensions.Shuffle(cards);
        return cards.ToArray();
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