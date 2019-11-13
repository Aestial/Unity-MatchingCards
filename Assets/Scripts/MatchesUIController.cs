using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesUIController : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;
    [SerializeField] PuzzleCreator creator;
    
    public void AddMatch(int type)
    {
        GameObject newMatch = Instantiate(prefab, container);
        CardImage cardImage = newMatch.GetComponent<CardImage>();
        cardImage.SetImage(creator.sprites[type].unlocked);
    }
    void Start()
    {
        
    }
}
