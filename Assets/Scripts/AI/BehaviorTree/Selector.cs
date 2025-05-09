using System.Collections.Generic;

public class Selector : InteriorNode
{
    public Selector(IEnumerable<BehaviorTree> children) : base(children)
    {
    }

    public override Result Run()
    {
        // Process children in order until one succeeds or is in progress
        while (current_child < children.Count)
        {
            Result result = children[current_child].Run();
            
            if (result == Result.SUCCESS)
            {
                Reset();  // Reset before returning success
                return Result.SUCCESS;
            }
            else if (result == Result.IN_PROGRESS)
            {
                return Result.IN_PROGRESS;  // Keep processing same child next time
            }
            
            // Only increment on failure
            current_child++;
        }

        // All children failed
        Reset();
        return Result.FAILURE;
    }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }

    private void Reset()
    {
        current_child = 0;  // Reset for next run
    }
}