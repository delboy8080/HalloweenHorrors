using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Image[] images;
    public void SetScore(int score)
    {
        scoreText.text= score.ToString();
    }

    public void UpdateLives(int lives)
    {
        if (lives == 3)
        {
            images[0].enabled = true;
            images[1].enabled = true;
            images[2].enabled = true;
        }
        else if (lives == 2)
        {
            images[0].enabled = false;
            images[1].enabled = true;
            images[2].enabled = true;
        }
        else if (lives == 1)
        {
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = true;
        }
        else if (lives == 0)
        {
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
        }
    }
}
