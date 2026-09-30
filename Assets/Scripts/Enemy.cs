using System;
using System.Collections;
using UnityEngine;

public class Enemy : Interactable
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int health = 3;
    [SerializeField] private int moveSpeed = 2;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int goldDrop = 10;

    // [CHANGE] static: the GameManager is unaware of the enemies (spawned by the WaveManager),
    // so it cannot subscribe to an instance Action.
    // [CHANGE] Action<int>: without a parameter, the GameManager cannot know the amount (goldDrop).
    public static Action<int> AddGold;

    // [ADDITION] Accessor: WaveManager.MoveEnemies() needs to read the speed.
    public int MoveSpeed => moveSpeed;
    
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Projectile projectile)) {
            TakeDamage(projectile.Damage);
            Destroy(projectile.gameObject);
        }
        else if (collider2D.TryGetComponent(out Hub hub)) {
            StartCoroutine(AttackHub(hub));
        }
    }

    // [ADDITION] Nothing in the diagram uses attackSpeed: the enemy attacks the Hub in a loop once it arrives there.
    private IEnumerator AttackHub(Hub hub)
    {
        while (hub != null) {
            hub.OnTakeDamage(damage);
            yield return new WaitForSeconds(1f / attackSpeed);
        }
    }

    private void TakeDamage(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        if (health <= 0)
            Death();
    }

    private void Death()
    {
        OnGoldAdd();
        Destroy(gameObject);
    }

    private void OnGoldAdd()
    {
        AddGold?.Invoke(goldDrop);
    }
}
