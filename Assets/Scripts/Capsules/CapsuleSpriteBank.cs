using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleSpriteBank : MonoBehaviour
{
    private static Dictionary<string, Sprite> _spriteDict = new();

    private void Awake()
    {
        Sprite[] capsuleSprites = Resources.LoadAll<Sprite>("Sprites/Capsules/Capsules_v1");
        _spriteDict = capsuleSprites.ToDictionary(sprite => sprite.name, sprite => sprite);
    }

    public static Sprite GetSprite(string name)
    {
        if (!_spriteDict.ContainsKey(name))
        {
            return null;
        }
        return _spriteDict[name];
    }
}
