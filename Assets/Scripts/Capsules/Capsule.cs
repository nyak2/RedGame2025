using System;
using UnityEngine;
using static CapsuleTier;

public class Capsule : MonoBehaviour
{
    /*private enum BudType
    {
        Tappy,
        Bam,
        Biggie,
        Ogu
    }*/

    private Tier _tier;

    private readonly static string[] _tiersAsString = Enum.GetNames(typeof(Tier));

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
            Merge(this, otherCapsule, firstContact.point);
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

    private Tier NextTier(Tier tier)
    {
        int tierIndex = Array.IndexOf(_tiersAsString, tier.ToString());
        int nextIndex = tierIndex + 1;
        if (nextIndex >= _tiersAsString.Length)
        {
            return tier;
        }

        return (Tier) Enum.Parse(typeof(Tier), _tiersAsString[nextIndex]);
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
