using System.Collections.Generic;

public class Selector : InteriorNode
{
    public override Result Run()
    {
        while (current_child < children.Count)
        {
            Result res = children[current_child].Run();
            if (res == Result.SUCCESS)
            {
                current_child = 0;
                return Result.SUCCESS;        }
            else if (res == Result.IN_PROGRESS)
            {
                return Result.IN_PROGRESS;
            }
            else if (res == Result.FAILURE)
            {
                current_child++;
            }
        }

        current_child = 0;
        return Result.FAILURE;
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children)
    {
    }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }

}
