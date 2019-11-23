using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] Image image;
    public void Set(Sprite sprite)
    {
        image.sprite = sprite;
    }    
}
