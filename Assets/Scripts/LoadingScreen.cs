using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadImage;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _loadBgImage;

    private Sequence _loadingSequence;
    private Sequence _rotateSequence;
    // Start is called before the first frame update
    void Start()
    {
        _loadImage.sprite = _sprites[Random.Range(0, _sprites.Count)];
        _loadImage.DOColor(new Color(_loadImage.color.r, _loadImage.color.g, _loadImage.color.b, 0), 0.0f);
        _loadBgImage.DOColor(new Color(_loadBgImage.color.r, _loadBgImage.color.g, _loadBgImage.color.b, 0), 0.0f);
        _loadBgImage.raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void EnableLoadingScreen()
    {
        _loadingSequence = DOTween.Sequence();
            
        _loadingSequence.Append(_loadBgImage.DOColor(new Color(_loadBgImage.color.r, _loadBgImage.color.g, _loadBgImage.color.b, 1.0f), 0.25f));
        _loadingSequence.Join(_loadImage.DOColor(new Color(_loadImage.color.r, _loadImage.color.g, _loadImage.color.b, 1.0f), 0.25f)).SetDelay(0.15f);
        _loadBgImage.raycastTarget = true;

        _rotateSequence = DOTween.Sequence();

        _rotateSequence.Append(_loadImage.transform
            .DOLocalRotate(new Vector3(0, 0, 20), 0.8f)
            .SetEase(Ease.InOutSine)).SetLoops(-1, LoopType.Yoyo);
    }

    public Sequence FadeLoadingBgIamge()
    {
        _loadingSequence = DOTween.Sequence();

        _loadingSequence.Append(_loadImage.DOColor(new Color(_loadImage.color.r, _loadImage.color.g, _loadImage.color.b, 0.0f), 0.25f));

        return _loadingSequence;
    }

    void TweenLoadImage()
    {

    }
}
