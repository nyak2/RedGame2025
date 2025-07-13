using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour, ISkill
{
    private const int _explodeAmount = 5;
    private SFXPlayer _sfxPlayer;

    private void Awake()
    {
        _sfxPlayer = GetComponent<SFXPlayer>();
    }

    public void Activate()
    {
        Explode();
    }

    private void Explode()
    {
        List<Capsule> randomCapsules = CapsulePooler.GetRandom(_explodeAmount);
        foreach (Capsule capsule in randomCapsules)
        {
            _sfxPlayer.PlaySfx(SFXLibrary.SFX_EXPLOSION_POP);
            CapsulePooler.Remove(capsule);
            Destroy(capsule.gameObject);
        }
    }
}
