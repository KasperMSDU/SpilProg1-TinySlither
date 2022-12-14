using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        resetScore();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetScore()
    {
        score = 0;
    }

    public void increaseScore(int increment)
    {
        score += increment;
        scoreText.text = score.ToString();
    }
}
