using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Canvas loginCanvas;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas leaderBoardCanvas;
    readonly Notifier notifier = new Notifier();
    public const string ON_PAUSE = "OnPause";
    public const string ON_LOGOUT = "OnLogout";
    public void HideLogin()
    {
        loginCanvas.enabled = false;
    }
    public void ShowLeaderboard(bool isShowing)
    {
        notifier.Notify(ON_PAUSE, isShowing);
        leaderBoardCanvas.enabled = isShowing;
        gameCanvas.enabled = !isShowing;
    }
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
        gameOverCanvas.enabled = puzzle.inProgress;
    }    
    private void HandleOnFinished(object[] args)
    {     
        gameOverCanvas.enabled = true;
    }
}
