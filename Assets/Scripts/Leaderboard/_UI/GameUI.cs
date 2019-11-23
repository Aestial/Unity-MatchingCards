using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text placeText;
    [SerializeField] Text nameText;
    [SerializeField] Text movesText;
    [SerializeField] Text timeText;
    [SerializeField] Text scoreText;    
    public void Set(Game game, int index)
    {
        Puzzle puzzle = game.puzzle;
        User user = game.user;
        Set(index, user.codename, puzzle.moves, puzzle.seconds, puzzle.score);        
    }
    public void Set(int index, string name, int moves, int time, int score)
    {
        placeText.text = index.ToString();
        nameText.text = name;
        movesText.text = moves.ToString();
        timeText.text = time.ToString();
        scoreText.text = score.ToString();
    }
}
