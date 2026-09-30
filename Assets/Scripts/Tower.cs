using UnityEngine;

public class Tower : Interactable
{
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Projectile projectile;
    private Enemy currentTarget;

    // [ADDITION] Without range, it is impossible to choose a target; without a price, money is useless.
    [SerializeField] private float range = 3f;
    [SerializeField] private int cost = 50;
    private float attackTimer;

    public int Cost => cost;

    // [ADDED] Nothing was calling Attack() or populating currentTarget.
    private void Update()
    {
        if (currentTarget == null || Vector2.Distance(transform.position, currentTarget.transform.position) > range)
            currentTarget = FindTarget();

        attackTimer += Time.deltaTime;
        if (currentTarget != null && attackTimer >= 1f / attackSpeed)
        {
            attackTimer = 0f;
            Attack();
        }
    }

    private Enemy FindTarget()
    {
        Enemy closest = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(transform.position, range))
        {
            if (!hit.TryGetComponent(out Enemy enemy))
                continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }
        return closest;
    }

    private void Attack()
    {
        Projectile instance = Instantiate(projectile, transform.position, Quaternion.identity);
        instance.SetTarget(currentTarget);
    }
}
