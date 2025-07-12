using System.Collections.Generic;
using UnityEngine;
using static CapsuleTier;

public class CapsuleChargeBank : MonoBehaviour
{
    private static Dictionary<Tier, float> _tierToChargeDict = new()
    {
        { Tier.One, 1.0f },
        { Tier.Two, 2.0f },
        { Tier.Three, 5.0f },
        { Tier.Four, 10.0f },
        { Tier.Five, 15.0f },
        { Tier.Six, 20.0f },
        { Tier.Seven, 25.0f },
        { Tier.Eight, 30.0f },
    };

    public static float GetCharge(Tier tier)
    {
        return _tierToChargeDict[tier];
    }
}
