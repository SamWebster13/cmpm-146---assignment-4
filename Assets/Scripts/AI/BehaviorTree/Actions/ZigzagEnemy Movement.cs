using UnityEngine;

public class ZigzagMoveToPlayer : BehaviorTree
{
    float arrived_distance;
    float amplitude;
    float frequency;
    float zigzagOffset; // Unique phase offset to avoid all enemies zigzagging identically

    public ZigzagMoveToPlayer(float arrived_distance, float amplitude = 1f, float frequency = 5f)
    {
        this.arrived_distance = arrived_distance;
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.zigzagOffset = Random.Range(0f, 100f); // Add randomness per instance
    }

    public override Result Run()
    {
        if (agent == null || GameManager.Instance.player == null)
            return Result.FAILURE;

        Vector3 toPlayer = GameManager.Instance.player.transform.position - agent.transform.position;
        if (toPlayer.magnitude < arrived_distance)
        {
            agent.GetComponent<Unit>().movement = Vector2.zero;
            return Result.SUCCESS;
        }

        // Get direction toward player
        Vector3 forward = toPlayer.normalized;

        // Get a perpendicular vector for sideways movement (zigzag)
        Vector3 right = Vector3.Cross(forward, Vector3.forward).normalized;

        // Apply sinusoidal offset for zigzag
        float zigzag = Mathf.Sin(Time.time * frequency + zigzagOffset) * amplitude;
        Vector3 zigzagDirection = (forward + right * zigzag).normalized;

        agent.GetComponent<Unit>().movement = zigzagDirection;
        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new ZigzagMoveToPlayer(arrived_distance, amplitude, frequency);
    }
}
