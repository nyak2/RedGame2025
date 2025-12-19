using System.Collections.Generic;
using UnityEngine;

public static class CapsuleMerger
{
    private static readonly HashSet<MergeRequest> _processingMergeRequests = new();

    public static void Merge(MergeRequest mergeRequest)
    {
        Capsule firstCapsule = mergeRequest.FirstCapsule;
        Capsule secondCapsule = mergeRequest.SecondCapsule;

        if (firstCapsule == null || secondCapsule == null)
        {
            return;
        }

        if (_processingMergeRequests.Contains(mergeRequest))
        {
            return;
        }

        _processingMergeRequests.Add(mergeRequest);
        CapsuleTier.Tier nextTier = CapsuleTier.NextTier(mergeRequest.CurrentCapsuleTier);
        float charge = CapsuleTier.GetCharge(mergeRequest.CurrentCapsuleTier);
        int score = CapsuleTier.GetScore(mergeRequest.CurrentCapsuleTier);

        firstCapsule.UpgradeToTier(nextTier);
        firstCapsule.transform.position = mergeRequest.ContactPoint;

        secondCapsule.Delete();

        DistributeParams(charge, score);
        _processingMergeRequests.Remove(mergeRequest);
    }

    private static void DistributeParams(float charge, int score)
    {
        ChargeKeeper.Instance.AddCharge(charge);
        ScoreKeeper.Instance.AddScore(score);
    }
}

public sealed class MergeRequest
{
    public Capsule FirstCapsule;
    public Capsule SecondCapsule;
    public CapsuleTier.Tier CurrentCapsuleTier;
    public Vector2 ContactPoint;

    public MergeRequest(Capsule firstCapsule, Capsule secondCapsule, Vector2 contactPoint)
    {
        FirstCapsule = firstCapsule;
        SecondCapsule = secondCapsule;
        CurrentCapsuleTier = firstCapsule.GetTier();
        ContactPoint = contactPoint;
    }

    public override bool Equals(object obj)
    {
        if (obj is not MergeRequest other)
        {
            return false;
        }
        return (FirstCapsule == other.FirstCapsule && SecondCapsule == other.SecondCapsule) ||
               (FirstCapsule == other.SecondCapsule && SecondCapsule == other.FirstCapsule);
    }

    public override int GetHashCode()
    {
        return FirstCapsule.GetHashCode() ^ SecondCapsule.GetHashCode();
    }
}
