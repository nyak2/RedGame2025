using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour, ISkill
{
    private const int _explodeAmount = 5;

    public void Activate()
    {
        Explode();
    }

    private void Explode()
    {
        List<Capsule> randomCapsules = CapsulePooler.GetRandom(_explodeAmount);
        foreach (Capsule capsule in randomCapsules)
        {
            CapsulePooler.Remove(capsule);
            Destroy(capsule.gameObject);
        }
    }
}
