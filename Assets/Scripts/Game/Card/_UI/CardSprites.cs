using System.Collections.Generic;
using UnityEngine;

public class CardSprites : Singleton<CardSprites>
{
    [Header("Directory path")]
    [SerializeField] string unlockedPath;
    [SerializeField] string lockedPath;
    public struct CardSprite
    {
        public Sprite unlocked;
        public Sprite locked;
    }
    public List<CardSprite> sprites = new List<CardSprite>();
    Sprite[] unlocked;
    Sprite[] locked;
    void Awake()
    {
        RetrieveAssets();
    }
    private void RetrieveAssets()
    {
        unlocked = Resources.LoadAll<Sprite>(unlockedPath);
        locked = Resources.LoadAll<Sprite>(lockedPath);
        for (int i = 0; i < unlocked.Length; i++)
        {
            var sprite = new CardSprite();
            sprite.unlocked = unlocked[i];
            sprite.locked = locked[i];
            sprites.Add(sprite);
        }
    }        
}
