using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private void Start()
    {
        GameManager.Instance.RemainingEnemy++;
    }
    private void Update()
    {
        Shooting();
    }
    public override void Shooting()
    {
        if (Time.time > fireTime)
        {
            CreateBullet(Bullet, transform.position, Vector3.zero);
            fireTime = Time.time + 1 / fireRate;
        }

    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Hp <= 0)
        {
            GameManager.Instance.EnemyIsDestroy();
            Destruction();
        }
    }


}
