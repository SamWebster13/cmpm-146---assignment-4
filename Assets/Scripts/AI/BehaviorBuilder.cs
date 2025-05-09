using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;

        if (agent.monster == "warlock")
        {
            result = new Selector(new BehaviorTree[]
            {
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("heal"),
                    new Heal()
                }),
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("buff"),
                    new StrengthFactorQuery(1.5f),
                    new Buff()
                }),
                new Sequence(new BehaviorTree[] {
                    // new ZigzagCirclePlayer(3f, 60f, 0.3f, 2f, 1.1f, 0.2f),
                    new ZigzagMoveToPlayer(36f, 8f, 2f),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                })
            });
        }
        else if (agent.monster == "zombie")
        {
            result = new Selector(new BehaviorTree[]
            {
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1,5f),
                    new Buff()
                }),
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(0,5f),
                    new AbilityReadyQuery("heal"),
                    new Heal()
                }),
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(4,15f),
                    // new ZigzagMoveToPlayer(18f, 6f, 1f),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),
            });
        }
        else // default enemy type
        {
            result = new Selector(new BehaviorTree[]
            {
                // new Sequence(new BehaviorTree[] {
                //     // new AbilityReadyQuery("buff"),
                //     new NearbyEnemiesQuery(1,4f),
                //     new Buff()
                // }),
                new Sequence(new BehaviorTree[] {
                    // new AbilityReadyQuery("buff"),
                    // new Buff(),
                    new NearbyEnemiesQuery(1,4f),
                    // new ZigzagMoveToPlayer(36f, 8f, 2f),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),
            });
        }

        foreach (var node in result.AllNodes())
        {
            node.SetAgent(agent);
        }

        return result;
    }
}
