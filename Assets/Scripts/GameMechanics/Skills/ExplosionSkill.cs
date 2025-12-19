using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour, ISkill
{
    private const int _explodeAmount = 5;
    private SFXPlayer _sfxPlayer;
    private bool _isActivated = false;

    private void Awake()
    {
        _sfxPlayer = GetComponent<SFXPlayer>();
    }

    public void Activate()
    {
        if (_isActivated)
        {
            return;
        }
        _isActivated = true;

        try
        {
            Explode();
        }
        finally
        {
            _isActivated = false;
        }
    }

    private void Explode()
    {
        List<Capsule> randomCapsules = CapsulePooler.GetRandom(_explodeAmount);
        _sfxPlayer.PlaySfx(SFXLibrary.SFX_EXPLOSION_POP);
        int chargeMultiplier = 0;
        int pointsMultiplier = 2;
        foreach (Capsule capsule in randomCapsules)
        {
            if (capsule == null)
            {
                continue;
            }
            capsule.Delete(chargeMultiplier, pointsMultiplier);
        }
    }
}
