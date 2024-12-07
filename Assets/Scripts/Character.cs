using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Mathf.Clamp(value, 0, 100);
        }
    }
    
    [SerializeField] protected float fireRate;
    protected float fireTime;

    public GameObject Bullet;
    public GameObject DestructionVFX;
    [SerializeField] private Image hpImage;
    [SerializeField] private int maxHp;

    public abstract void Shooting();

    public void CreateBullet(GameObject obj, Vector3 position, Vector3 rotation)
    {
        Instantiate(obj, position, Quaternion.Euler(rotation));
    }
    public virtual void TakeDamage(int damage)
    {
        Hp -= damage;
        hpImage.fillAmount = (float)Hp / maxHp;
    }
    public void Destruction()
    {
        Instantiate(DestructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
