using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeneUIManager : MonoBehaviour
{
    [SerializeField] string _gameSceneName;
    [SerializeField] LoadingScreen _loadingScreen;
    [SerializeField] LeaderBoardManager _leaderBoardManager;

    private Sequence _loadGameSequence;

    private void Awake()
    {

    }
    public void PlayGame()
    {
        StartCoroutine(LoadGameScene());
    }

    public void ShowLeaderBoarad()
    {
        _leaderBoardManager.ShowLeaderBoard();
    }

    public void HideLeaderBoarad()
    {
        _leaderBoardManager.HideLeaderBoard(0.25f);
    }

    public IEnumerator LoadGameScene()
    {
        _loadingScreen.EnableLoadingScreen();
        yield return new WaitForSeconds(1.5f);

        _loadGameSequence = DOTween.Sequence();

        _loadGameSequence.Append(_loadingScreen.FadeLoadingBgIamge());

        _loadGameSequence.OnComplete(() =>
        {
            SceneManager.LoadScene(_gameSceneName);
        });
    }
}
