using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas loginCanvas;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas leaderBoardCanvas;
    Notifier notifier = new Notifier();
    public void ShowLeaderboard()
    {
        leaderBoardCanvas.enabled = true;
        gameCanvas.enabled = false;
    }
    public void ShowGame()
    {
        gameCanvas.enabled = true;
        leaderBoardCanvas.enabled = false;
    }
    void Start()
    {
        notifier.Subscribe(PuzzleLoader.ON_LOADED, HandleOnLoaded);
        notifier.Subscribe(PuzzleController.ON_FINISHED, HandleOnFinished);
    }
    private void HandleOnLoaded(object[] args)
    {        
        gameOverCanvas.enabled = false;
    }
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    private void HandleOnFinished(object[] args)
    {     
        gameOverCanvas.enabled = true;
    }
    void Update()
    {
        
    }
}
