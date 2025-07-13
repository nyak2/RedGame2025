using DG.Tweening;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _duration = 1.5f;
    private Vector3 _originalPos;
    private float _dropYPosA;
    private float _dropYPosB;

    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _originalPos = transform.localPosition;
        _dropYPosA = _originalPos.y - 1.5f;
        _dropYPosB = _dropYPosA - 1.0f;
    }

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        DisableLine();
    }

    public void EnableLine()
    {
        _boxCollider.enabled = true;
    }

    public void DisableLine()
    {
        _boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Capsule"))
        {
            _gameManager.InvokeOnLoseEvent();
        }
    }

    public void Drop()
    {
        if (transform.localPosition.y > _dropYPosA)
        {
            transform.DOLocalMoveY(_dropYPosA, _duration, true).SetEase(Ease.OutCubic);
            return;
        }

        if (transform.localPosition.y > _dropYPosB)
        {
            transform.DOLocalMoveY(_dropYPosB, _duration, true).SetEase(Ease.OutCubic);
            return;
        }
    }
}
