using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private int bulletPower = 2;
    void Start()
    {
        Initialize(-4, false);
    }
    public override void Update()
    {
        base.Update();
    }
    public bool IsEggDestroy()
    {
        bulletPower--;
        if(bulletPower == 0)
        {
            return true;
        }
        return false;
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayerBullet && collision.tag == "Player")
        {
            collision.GetComponent<Character>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }




}
