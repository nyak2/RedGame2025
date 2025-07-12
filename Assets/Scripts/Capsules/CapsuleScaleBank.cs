using System.Collections.Generic;
using UnityEngine;
using static CapsuleTier;

public class CapsuleScaleBank : MonoBehaviour
{
    private readonly static Dictionary<Tier, float> _tierToScaleDict = new()
    {
        { Tier.One, 1.0f },
        { Tier.Two, 1.2f },
        { Tier.Three, 1.5f },
        { Tier.Four, 1.8f },
        { Tier.Five, 2.1f },
        { Tier.Six, 2.4f },
        { Tier.Seven, 2.7f },
        { Tier.Eight, 3.0f }
    };

    public static float GetScale(Tier tier)
    {
        return _tierToScaleDict[tier];
    }
}
