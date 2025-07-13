using DG.Tweening;
using UnityEngine;

public class PoofAnimation : MonoBehaviour
{
    [SerializeField, Min(0.0f)] private Vector3 _initialScale;
    [SerializeField, Min(0.0f)] private Vector3 _finalScale;
    [SerializeField] private float _duration = 3.0f;

    private void Awake()
    {
        Animate();
    }

    public void Animate()
    {
        transform.localScale = _initialScale;
        transform.DOScale(_finalScale, _duration).SetEase(Ease.OutCubic)
            .OnComplete(() => Destroy(gameObject));
    }
}
