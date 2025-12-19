using System.Collections.Generic;
using UnityEngine;

public class CapsulePooler : MonoBehaviour
{
    private readonly static List<Capsule> _capsules = new();

    public static void Pool(Capsule capsule)
    {
        _capsules.Add(capsule);
    }

    public static void Remove(Capsule capsule)
    {
        _capsules.Remove(capsule);
    }

    public static List<Capsule> GetRandom(int count)
    {
        List<Capsule> validCapsules = _capsules.FindAll(capsule => capsule.IsLanded);
        List<Capsule> resultCapsules = new();

        if (validCapsules.Count == 0)
        {
            return resultCapsules;
        }

        int clampedCount = Mathf.Clamp(count, 0, validCapsules.Count);
        for (int i = 0; i < clampedCount; i++)
        {
            int randIdx = Random.Range(0, validCapsules.Count);
            resultCapsules.Add(validCapsules[randIdx]);
        }
        return resultCapsules;
    }
}
