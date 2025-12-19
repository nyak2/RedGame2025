using System.Collections.Generic;
using UnityEngine;
using static CapsuleTier;

public class CapsuleChargeBank : MonoBehaviour
{
    private static Dictionary<Tier, float> _tierToChargeDict = new()
    {
        { Tier.One, 5.0f },
        { Tier.Two, 10.0f },
        { Tier.Three, 15.0f },
        { Tier.Four, 20.0f },
        { Tier.Five, 25.0f },
        { Tier.Six, 30.0f },
        { Tier.Seven, 35.0f },
        { Tier.Eight, 40.0f },
    };

    public static float GetCharge(Tier tier)
    {
        return _tierToChargeDict[tier];
    }
}
