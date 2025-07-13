using UnityEngine;
using static CapsuleTier;

public class Capsule : MonoBehaviour
{
    private Tier _tier;

    // Components.
    private SpriteRenderer _spriteRenderer;

    [HideInInspector] public bool _isLanded;

    private bool _isTriggered;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize(RandomCapsuleGenerator.GetRandomTier());
        _isTriggered = false;
    }

    private void Initialize(Tier tier)
    {
        _tier = tier;

        _spriteRenderer.sprite = GetSprite(_tier);
        float newScale = GetScale(_tier);
        transform.localScale = new(newScale, newScale, 1.0f);
        _isLanded = false;
        tag = "Untagged";
    }

    public Tier GetTier()
    {
        return _tier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.TryGetComponent<Capsule>(out var capsule) && !_isLanded)
        {
            _isLanded = true;
        }

        if (_isTriggered)
        {
            return;
        }

        if (!collision.gameObject.TryGetComponent<Capsule>(out var otherCapsule))
        {
            return;
        }


        if(!_isLanded)
        {
            return;
        }

        ContactPoint2D firstContact = collision.GetContact(0);
        bool isSameCapsuleType = Equals(otherCapsule);
        if (isSameCapsuleType)
        {
            float charge = GetCharge(_tier);
            int score = GetScore(_tier);
            Merge(this, otherCapsule, firstContact.point);
            DistributeParams(charge, score);
            _isTriggered = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.TryGetComponent<Capsule>(out var capsule) && !_isLanded)
        {
            _isLanded = true;
        }

        if (_isTriggered)
        {
            return;
        }

        if (!collision.gameObject.TryGetComponent<Capsule>(out var otherCapsule))
        {
            return;
        }

        if (!_isLanded)
        {
            return;
        }

        ContactPoint2D firstContact = collision.GetContact(0);
        bool isSameCapsuleType = Equals(otherCapsule);
        if (isSameCapsuleType)
        {
            float charge = GetCharge(_tier);
            int score = GetScore(_tier);
            Merge(this, otherCapsule, firstContact.point);
            DistributeParams(charge, score);
            _isTriggered = true;

        }
    }

    private void Merge(Capsule thisCapsule, Capsule otherCapsule, Vector2 contactPoint)
    {
        Tier nextTier = NextTier(thisCapsule._tier);
        if (nextTier == Tier.Max)
        {
            return;
        }

        Initialize(nextTier);
        Destroy(otherCapsule.gameObject);
        transform.position = contactPoint;
    }

    private void DistributeParams(float charge, int score)
    {
        ChargeKeeper.Instance.AddCharge(charge);
        ScoreKeeper.Instance.AddScore(score);
    }

    public bool Equals(Capsule other)
    {
        if (other == null)
        {
            return false;
        }

        return _tier == other._tier;
    }
}
