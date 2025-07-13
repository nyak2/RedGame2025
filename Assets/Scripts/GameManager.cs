using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DropController _dropController;
    [SerializeField] private SlideController _slideController;
    [SerializeField] private GameOverUI _gameOverScreen;
    [SerializeField] private TimeKeeper _timeKeeper;
    private SFXPlayer _sfxPlayer;
    public delegate void OnDropped();
    public event OnDropped OnDroppedEvent;

    public delegate void OnLose();
    public event OnLose OnLoseEvent;

    private PlayerData _loadedPlayerData;

    private void Awake()
    {
        _sfxPlayer = GetComponent<SFXPlayer>();
        _loadedPlayerData = DataSaver.Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDroppedEvent += SpawnNewCapsule;
        OnLoseEvent += GameOver;
    }

    private void OnDestroy()
    {
        OnDroppedEvent -= SpawnNewCapsule;
        OnLoseEvent -= GameOver;
    }

    void SpawnNewCapsule()
    {
        _dropController.InitializeCapsule();
    }

    public void InvokeOnDroppedEvent()
    {
        OnDroppedEvent?.Invoke();
    }

    public void GameOver()
    {
        CanControlControllers(false);
        _timeKeeper.StopTimer();

        int totalScore = ScoreKeeper.GetScore();
        int newHighScore = totalScore > _loadedPlayerData.HighScore ? totalScore : _loadedPlayerData.HighScore;
        ShowGameOverScreen(totalScore, newHighScore);
        SaveNewData(totalScore, newHighScore);
    }

    private void ShowGameOverScreen(int totalScore, int newHighScore)
    {
        _sfxPlayer.PlaySfx(SFXLibrary.SFX_GAME_OVER);
        _gameOverScreen.UpdateUi(totalScore, newHighScore);
        _gameOverScreen.gameObject.SetActive(true);
    }

    private void SaveNewData(int totalScore, int newHighScore)
    {
        PlayerData updatedData = new()
        {
            Username = _loadedPlayerData.Username,
            TotalScore = totalScore,
            HighScore = newHighScore
        };
        DataSaver.Save(updatedData);
    }

    public void InvokeOnLoseEvent()
    {
        OnLoseEvent?.Invoke();
    }

    public void CanControlControllers(bool canControl)
    {
        _dropController.CanControl(canControl);
        _slideController.CanControl(canControl);
    }
}
