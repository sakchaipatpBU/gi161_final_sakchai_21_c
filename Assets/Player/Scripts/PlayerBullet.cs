using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    void Start()
    {
        Initialize(3, true);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Egg")
        {
            Destroy(gameObject);
            if (collision.GetComponent<EnemyBullet>().IsEggDestroy())
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
