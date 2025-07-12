using System.Collections.Generic;
using UnityEngine;
using static CapsuleTier;

public class CapsuleScoreBank : MonoBehaviour
{
    private readonly static Dictionary<Tier, int> _tierToScoreDict = new()
    {
        { Tier.One, 2 },
        { Tier.Two, 10 },
        { Tier.Three, 20 },
        { Tier.Four, 32 },
        { Tier.Five, 46 },
        { Tier.Six, 62 },
        { Tier.Seven, 80 },
        { Tier.Eight, 100 }
    };

    public static int GetScore(Tier tier)
    {
        return _tierToScoreDict[tier];
    }
}
