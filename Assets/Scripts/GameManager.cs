using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //declaring and initializing variables
    private int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //calling "resetScore" function
        ResetScore();

        //assigning the scoreText variable to the text on the "Score" object;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        //if scoreText is not null:
        if (scoreText != null)
        {
            //set the scoreText.text to the score converted to a string
            scoreText.text = score.ToString();
        }

    }

    //"ResetScore" method, that resets the score
    public void ResetScore()
    {
        //Setting score back to 0
        score = 0;
    }

    //"IncreaseScore" method that increses the score by the amount parsed in as a parameter
    public void IncreaseScore(int increment)
    {
        //incresing score with "increment"
        score += increment;

        //setting the scoreText.text to the new score
        scoreText.text = score.ToString();
    }
}
