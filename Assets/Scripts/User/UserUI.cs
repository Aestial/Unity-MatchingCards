using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    [SerializeField] Text placeText;
    [SerializeField] Text nameText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;

    public void Set(int place, string name, int score, int seconds)
    {
        placeText.text = place.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();
        timeText.text = seconds.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
