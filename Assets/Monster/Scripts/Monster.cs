using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Monster : ScriptableObject
{    
    public Color color;
    public Sprite face;
    public AudioClip fx;

    public void Print ()
    {
        Debug.Log("Color: " + color + "Face: " +  face + "Sound: " + fx);
    }
}