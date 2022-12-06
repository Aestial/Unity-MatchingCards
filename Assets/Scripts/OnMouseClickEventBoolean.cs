using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool>
{
}

public class OnMouseClickEventBoolean : MonoBehaviour
{
    [SerializeField] private BoolEvent onMouseDown;
    
    public bool value;

    void OnMouseDown() 
    {
        value = !value;
        onMouseDown.Invoke(value);
    }
    void Start()
    {
        value = false;
    }

}
