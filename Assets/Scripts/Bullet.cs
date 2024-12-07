using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour , IDirectMovable
{
    [SerializeField] protected bool isPlayerBullet;
    
    [SerializeField] private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = Mathf.Clamp(value, 0, 5);
        }
    }
    [SerializeField] private float moveSpeed;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = Mathf.Clamp(value, -10f, 10f);
        }
    }

    public virtual void Update()
    {
        Moving();
    }
    public void Initialize(float moveSpeed, bool isPlayerBullet)
    {
        MoveSpeed = moveSpeed;
        this.isPlayerBullet = isPlayerBullet;
    }

    public void Moving()
    {
        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
    }

    public abstract void OnTriggerEnter2D(Collider2D collision);
    
}