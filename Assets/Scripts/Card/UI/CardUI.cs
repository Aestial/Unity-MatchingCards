using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] Image image;
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }    
}
