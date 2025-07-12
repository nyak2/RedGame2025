using UnityEngine;
using static CapsuleTier;

public class Capsule : MonoBehaviour
{
    private Tier _tier;

    // Components.
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize(RandomCapsuleGenerator.GetRandomTier());
    }

    private void Initialize(Tier tier)
    {
        _tier = tier;

        _spriteRenderer.sprite = GetSprite(_tier);
        float newScale = GetScale(_tier);
        transform.localScale = new(newScale, newScale, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Capsule>(out var otherCapsule))
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
