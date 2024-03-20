using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverReasonText;
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        DisplayGameOverInfo();
    }

    void DisplayGameOverInfo()
    {
        string gameOverReason = PlayerPrefs.GetString("GameOverReason", "Unknown Reason");
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        gameOverReasonText.text = "Defeat Reason: " + gameOverReason;
        finalScoreText.text = "Final Score: " + finalScore.ToString();
    }
}
