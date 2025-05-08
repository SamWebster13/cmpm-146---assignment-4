using UnityEngine;

public class HasNearbyAllyQuery : BehaviorTree
{
    private float radius;

    public HasNearbyAllyQuery(float radius)
    {
        this.radius = radius;
    }

    public override Result Run()
    {
        var enemies = GameManager.Instance.GetEnemiesInRange(agent.transform.position, radius);

        foreach (var e in enemies)
        {
            if (e != agent && Vector3.Distance(e.transform.position, agent.transform.position) <= radius)
            {
                return Result.SUCCESS;
            }
        }
        return Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new HasNearbyAllyQuery(radius);
    }
}
