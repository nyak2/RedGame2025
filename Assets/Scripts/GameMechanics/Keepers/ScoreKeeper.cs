using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private static int _score = 0;
    [SerializeField] private TextMeshProUGUI _scoreTextMesh;
    public static ScoreKeeper Instance;

    private void Awake()
    {
        Instance = this;
        UpdateScoreUi();
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScoreUi();
    }

    public void UpdateScoreUi()
    {
        _scoreTextMesh.text = _score.ToString();
    }

    public static int GetScore()
    {
        return _score;
    }
}
