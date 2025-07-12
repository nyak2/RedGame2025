using System;
using UnityEngine;

public class CapsuleTier : MonoBehaviour
{
    public enum Tier
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Max
    }

    private readonly static string[] _tiersAsString = Enum.GetNames(typeof(Tier));

    public static Tier NextTier(Tier tier)
    {
        int tierIndex = Array.IndexOf(_tiersAsString, tier.ToString());
        int nextIndex = tierIndex + 1;
        if (nextIndex >= _tiersAsString.Length)
        {
            return tier;
        }

        return (Tier)Enum.Parse(typeof(Tier), _tiersAsString[nextIndex]);
    }

    public static float GetScale(Tier tier)
    {
        return CapsuleScaleBank.GetScale(tier);
    }

    public static Sprite GetSprite(Tier tier)
    {
        return CapsuleSpriteBank.GetSprite(tier.ToString());
    }

    public static float GetCharge(Tier tier)
    {
        return CapsuleChargeBank.GetCharge(tier);
    }

    public static int GetScore(Tier tier)
    {
        return CapsuleScoreBank.GetScore(tier);
    }
}
