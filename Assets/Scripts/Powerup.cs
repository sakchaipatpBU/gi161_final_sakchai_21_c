using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IDirectMovable
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }

    void Update()
    {
        Moving();
    }
    public void Moving()
    {
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Player.Instance.CheckWeaponPower())
            {
                Player.Instance.WeaponPower++;
            }
            Destroy(gameObject);
        }
    }

}
