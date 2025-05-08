using UnityEngine;

public class ZigzagCirclePlayer : BehaviorTree
{
    private float radius;
    private float speed;
    private float amplitude;
    private float frequency;
    private float angle;
    private float zigzagOffset;
    private float minRadius;
    private float shrinkRate;

    public ZigzagCirclePlayer(float startRadius = 5f, float speed = 90f, float amplitude = 0.5f, float frequency = 2f, float minRadius = 1.5f, float shrinkRate = 0.5f)
    {
        this.radius = startRadius;
        this.speed = speed;
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.minRadius = minRadius;
        this.shrinkRate = shrinkRate;
        this.angle = 0f;
        this.zigzagOffset = Random.Range(0f, 100f);
    }

    public override Result Run()
    {
        if (agent == null || GameManager.Instance.player == null)
            return Result.FAILURE;

        // Slowly reduce the radius, but not below the minimum
        radius = Mathf.Max(minRadius, radius - shrinkRate * Time.deltaTime);

        angle += speed * Time.deltaTime;
        float baseRad = angle * Mathf.Deg2Rad;

        float zigzag = Mathf.Sin(Time.time * frequency + zigzagOffset) * amplitude;

        Vector3 forward = new Vector3(Mathf.Cos(baseRad), 0, Mathf.Sin(baseRad));
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

        Vector3 offset = forward * radius + right * zigzag;
        Vector3 targetPos = GameManager.Instance.player.transform.position + offset;

        agent.GetComponent<Unit>().movement = (targetPos - agent.transform.position).normalized;

        return Result.IN_PROGRESS;
    }

    public override BehaviorTree Copy()
    {
        return new ZigzagCirclePlayer(radius, speed, amplitude, frequency, minRadius, shrinkRate);
    }
}
