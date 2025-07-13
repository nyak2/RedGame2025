using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DropController _dropController;
    [SerializeField] private SlideController _slideController;
    [SerializeField] private GameOverUI _gameOverScreen;
    private SFXPlayer _sfxPlayer;
    public delegate void OnDropped();
    public event OnDropped OnDroppedEvent;

    public delegate void OnLose();
    public event OnLose OnLoseEvent;

    private void Awake()
    {
        _sfxPlayer = GetComponent<SFXPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDroppedEvent += SpawnNewCapsule;
        OnLoseEvent += ShowGameOverScreen;
    }

    private void OnDestroy()
    {
        OnDroppedEvent -= SpawnNewCapsule;
        OnLoseEvent -= ShowGameOverScreen;
    }

    void SpawnNewCapsule()
    {
        _dropController.InitializeCapsule();
    }

    public void InvokeOnDroppedEvent()
    {
        OnDroppedEvent?.Invoke();
    }

    public void ShowGameOverScreen()
    {
        _sfxPlayer.PlaySfx(SFXLibrary.SFX_GAME_OVER);
        int totalScore = ScoreKeeper.GetScore();
        CanControlControllers(false);
        _gameOverScreen.gameObject.SetActive(true);
        _gameOverScreen.UpdateUi(totalScore, 0);
        //show screen here
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
