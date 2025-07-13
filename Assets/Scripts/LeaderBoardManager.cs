using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private Image _panel;

    private Sequence _hideSequqnce;
    // Start is called before the first frame update
    void Start()
    {
        HideLeaderBoard(0.0f);
    }

    public void ShowLeaderBoard()
    {
        _panel.gameObject.SetActive(true);
        gameObject.transform.DOScale(1.0f, 0.25f);
    }

    public void HideLeaderBoard(float _hideDuration)
    {
        _hideSequqnce = DOTween.Sequence();

        _hideSequqnce.Append(gameObject.transform.DOScale(0.0f, _hideDuration));

        _hideSequqnce.OnComplete(() =>
        {
            _panel.gameObject.SetActive(false);
        });
    }
}
