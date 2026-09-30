using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float moveSpeed = 8f;

    // [ADDITION] Accessor: Enemy enabled damage in its OnTriggerEnter2D.
    public int Damage => damage;

    // [ADDITION] Without a target or a method, a projectile cannot move.
    private Enemy target;

    public void SetTarget(Enemy target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
}
