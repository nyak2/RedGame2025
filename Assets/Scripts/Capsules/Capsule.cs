using UnityEngine;
using static CapsuleTier;

public class Capsule : MonoBehaviour
{
    private Tier _tier;

    // Components.
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private PoofAnimation _poof;

    [HideInInspector] public bool _isLanded;
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

    public Tier GetTier()
    {
        return _tier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isOtherCollisionCapsule = collision.gameObject.TryGetComponent<Capsule>(out var otherCapsule);
        if (collision.gameObject.CompareTag("Ground") || (isOtherCollisionCapsule && !_isLanded))
        {
            _isLanded = true;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        bool isOtherCollisionCapsule = collision.gameObject.TryGetComponent<Capsule>(out var otherCapsule);
        if (collision.gameObject.CompareTag("Ground") || (isOtherCollisionCapsule && !_isLanded))
        {
            _isLanded = true;
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

        _sfxPlayer.PlaySfx(SFXLibrary.SFX_CAPSULES_MERGE);
        otherCapsule.Delete();
        SpawnPoof();
        Initialize(nextTier);
        transform.position = contactPoint;
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

    public void Delete()
    {
        SpawnPoof();
        gameObject.SetActive(false);
        CapsulePooler.Remove(this);
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
