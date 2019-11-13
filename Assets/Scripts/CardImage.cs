using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImage : MonoBehaviour
{
    [SerializeField] Image image;
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
