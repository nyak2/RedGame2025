using System;
using System.Collections;
using UnityEngine;
using static CapsuleTier;

public class Capsule : MonoBehaviour
{
    private Tier _tier;

    // Components.
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private PoofAnimation _poof;

    [HideInInspector] private bool _isLanded = false;
    public bool IsLanded => _isLanded;
    public event Action<Capsule> OnLanded;

    private SFXPlayer _sfxPlayer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sfxPlayer = GetComponent<SFXPlayer>();
        CapsulePooler.Pool(this);
        Initialize(RandomCapsuleGenerator.GetRandomTier());
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

    public void Reinitialize(Tier tier)
    {
        SpawnPoof();
        Initialize(tier);
    }

    public Tier GetTier()
    {
        return _tier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCapsuleCollide(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCapsuleCollide(collision);
    }

    private void OnCapsuleCollide(Collision2D collision)
    {
        bool isOtherCollisionCapsule = collision.gameObject.TryGetComponent<Capsule>(out var otherCapsule);
        if (!_isLanded && (collision.gameObject.CompareTag("Ground") || isOtherCollisionCapsule))
        {
            _isLanded = true;
            gameObject.tag = "Capsule";
            OnLanded?.Invoke(this);
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

        _sfxPlayer.PlaySfx(SFXLibrary.SFX_CAPSULES_MERGE);
        MergeRequest mergeRequest = new(thisCapsule, otherCapsule, contactPoint);
        CapsuleMerger.Merge(mergeRequest);
    }

    public void UpgradeToTier(Tier tier)
    {
        if (tier == Tier.Max)
        {
            return;
        }
        Reinitialize(tier);
    }

    private void DistributeParams(float charge, int score)
    {
        ChargeKeeper.Instance.AddCharge(charge);
        ScoreKeeper.Instance.AddScore(score);
    }

    private void SpawnPoof()
    {
        Instantiate(_poof, transform.position, Quaternion.identity);
    }

    public void Delete(int chargeMultipler, int scoreMultiplier)
    {
        DistributeParams(GetCharge(_tier) * chargeMultipler, GetScore(_tier) * scoreMultiplier);  
        Delete();
    }

    public void Delete()
    {
        SpawnPoof();
        Disable();
    }

    private void Disable()
    {
        CapsulePooler.Remove(this);
        Destroy(gameObject);
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
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
