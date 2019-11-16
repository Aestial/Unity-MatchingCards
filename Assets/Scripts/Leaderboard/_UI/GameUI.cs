using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text placeText;
    [SerializeField] Text nameText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    public void Set(Game game)
    {
        placeText.text = "X";
        nameText.text = game.user.codename;
        scoreText.text = game.score.ToString();
        timeText.text = game.time.ToString();
    }
    public void Set(int place, string name, int score, int seconds)
    {
        placeText.text = place.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();
        timeText.text = seconds.ToString();
    }
}
