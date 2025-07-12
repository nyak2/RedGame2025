using System.Collections.Generic;
using UnityEngine;
using static Capsule;

public class RandomCapsuleGenerator : MonoBehaviour
{
    private readonly static List<Tier> _possibleTiers = new()
    {
        Tier.One,
        Tier.Two,
        Tier.Three,
    };

    public static Tier GetRandomTier()
    {
        int randIdx = Random.Range(0, _possibleTiers.Count);
        return _possibleTiers[randIdx];
    }
}
