using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public void SetScore(int score)
    {
        scoreText.text= score.ToString();
    }
}
