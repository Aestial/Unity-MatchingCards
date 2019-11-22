using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] bool enabledOnAwake;
    Canvas canvas;
    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = enabledOnAwake;
    }    
}
