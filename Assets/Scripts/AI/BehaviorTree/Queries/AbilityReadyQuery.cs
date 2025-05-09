public class AbilityReadyQuery : BehaviorTree
{
    string ability;

    public override Result Run()
    {
        var action = agent.GetAction(ability);
        if (action != null && action.Ready())
        {
            return Result.SUCCESS;
        }
        return Result.FAILURE;
    }

    public AbilityReadyQuery(string ability) : base()
    {
        this.ability = ability;
    }

    public override BehaviorTree Copy()
    {
        return new AbilityReadyQuery(ability);
    }
}
