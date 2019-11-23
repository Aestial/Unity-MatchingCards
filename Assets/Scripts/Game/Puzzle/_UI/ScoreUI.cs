using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] string scorePrefix;
    // Notifier
    readonly Notifier notifier = new Notifier();
    void Awake()
    {
        notifier.Subscribe(PuzzleLoader.ON_LOADED, HandleOnLoaded);
        notifier.Subscribe(PuzzleController.ON_FINISHED, HandleOnFinished);
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnLoaded(object[] args)
    {
        Puzzle puzzle = (Puzzle)args[0];
        scoreText.text = scorePrefix + puzzle.score;
    }
    private void HandleOnFinished(object[] args)
    {
        Game game = (Game)args[0];
        scoreText.text = scorePrefix + game.puzzle.score;
    }    
}
