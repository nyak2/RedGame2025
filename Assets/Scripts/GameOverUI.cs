using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalScoreTextMesh;
    [SerializeField] private TextMeshProUGUI _highScoreTextMesh;

    public void UpdateUi(int totalScore, int highScore)
    {
        _totalScoreTextMesh.text = totalScore.ToString();
        _highScoreTextMesh.text = highScore.ToString();
    }
}
