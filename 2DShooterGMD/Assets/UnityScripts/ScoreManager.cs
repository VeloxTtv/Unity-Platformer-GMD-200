using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreDisplay();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }
    void UpdateScoreDisplay()
    {
        scoreText.text = "" + score;
    }
}