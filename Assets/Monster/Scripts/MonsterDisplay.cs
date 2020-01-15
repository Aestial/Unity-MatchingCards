using UnityEngine;

public class MonsterDisplay : CardDisplay
{
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite frontSprite;
    [SerializeField] SpriteRenderer backgroundSR;
    [SerializeField] SpriteRenderer frontSR;
    public Monster monster;
    public override void GetAsset(int index)
    {
        monster = MonsterLoad.Instance.monsters[index];        
        frontSR.sprite = monster.face;
        base.Set(monster.fx);
    }
    public override void Turn(CardState state)
    {
        switch (state)
        {
            case CardState.Visible:
                PlayFX();
                backgroundSR.color = monster.color;
                backgroundSR.sprite = frontSprite;
                frontSR.enabled = true;
                break;
            case CardState.Matched:                
                backgroundSR.color = monster.color;
                backgroundSR.sprite = frontSprite;               
                frontSR.enabled = true;
                break;
            case CardState.Invisible:
                backgroundSR.color = Color.white;
                backgroundSR.sprite = backSprite;                
                frontSR.enabled = false;
                break;
            case CardState.Disabled:
                backgroundSR.color = Color.gray;
                backgroundSR.sprite = backSprite;
                frontSR.enabled = false;
                break;
        }
    }    
}