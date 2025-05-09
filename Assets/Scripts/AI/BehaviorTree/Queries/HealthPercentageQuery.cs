public class HealthPercentageQuery : BehaviorTree
{
    private float threshold;
    
    public HealthPercentageQuery(float threshold) {
        this.threshold = threshold;
    }
    
    public override Result Run()
    {
        var target = GameManager.Instance.GetClosestOtherEnemy(agent.gameObject);
        if (target == null) return Result.FAILURE;
        
        EnemyController enemy = target.GetComponent<EnemyController>();
        return (enemy.currentHealth / enemy.maxHealth <= threshold) 
            ? Result.SUCCESS 
            : Result.FAILURE;
    }
    
    public override BehaviorTree Copy() => new HealthPercentageQuery(threshold);
}