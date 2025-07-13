using System.Collections.Generic;
using UnityEngine;
using static CapsuleTier;

public class CapsuleScoreBank : MonoBehaviour
{
    private readonly static Dictionary<Tier, int> _tierToScoreDict = new()
    {
        { Tier.One, 3 },
        { Tier.Two, 15 },
        { Tier.Three, 30 },
        { Tier.Four, 48 },
        { Tier.Five, 69 },
        { Tier.Six, 93 },
        { Tier.Seven, 120 },
        { Tier.Eight, 150 }
    };

    public static int GetScore(Tier tier)
    {
        return _tierToScoreDict[tier];
    }
}
