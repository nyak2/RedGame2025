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

    public static float GetScale(Tier tier)
    {
        return CapsuleScaleBank.GetScale(tier);
    }

    public static Sprite GetSprite(Tier tier)
    {
        return CapsuleSpriteBank.GetSprite(tier.ToString());
    }

}
