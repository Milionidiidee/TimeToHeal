using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody2D body;
    float bornTime;
    public Vector2 Speed;

    [SerializeField] int _damageToPlayer;
    [SerializeField] float _lifeSpan = 8f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        bornTime = Time.time;
    }

    private void Update()
    {
        body.velocity = Speed;
        if (Time.time - bornTime > _lifeSpan) Explode();
    }

    void Explode()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        EnemyController enemy = go.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage();
            Explode();
            return;
        }
        PlayerHealth playerHealth = go.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.DecreaseHealth(_damageToPlayer);
            Explode();
            return;
        }

        Explode();
    }
}
