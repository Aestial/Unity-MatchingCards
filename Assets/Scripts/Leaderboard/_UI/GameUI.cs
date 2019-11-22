using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text placeText;
    [SerializeField] Text nameText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    public void Set(Game game, int index)
    {        
        nameText.text = game.user.codename;
        scoreText.text = game.score.ToString();
        timeText.text = game.time.ToString();
        placeText.text = index.ToString();
    }
    public void Set(int index, string name, int score, int seconds)
    {
        placeText.text = index.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();
        timeText.text = seconds.ToString();
    }
}
