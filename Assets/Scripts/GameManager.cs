using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DropController _dropController;
    [SerializeField] private SlideController _slideController;
    [SerializeField] private GameOverUI _gameOverScreen;
    [SerializeField] private TimeKeeper _timeKeeper;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _cooldownScreen;
    private float _countdownTimer = 3;
    private bool _countdownDone;
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
        _timeText.text = _countdownTimer.ToString("F0");
        _cooldownScreen.SetActive(true);
        CanControlControllers(false);

        OnDroppedEvent += SpawnNewCapsule;
        OnLoseEvent += GameOver;
    }

    private void Update()
    {
        if(_countdownDone)
        {
            return;
        }

         _countdownTimer -= Time.deltaTime;
        _timeText.text = _countdownTimer.ToString("F0");

        if(_countdownTimer <= 0)
        {
            _countdownDone = true;
            _cooldownScreen.SetActive(false);
            CanControlControllers(true);
            _timeKeeper.StartTimer();
        }
        
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
