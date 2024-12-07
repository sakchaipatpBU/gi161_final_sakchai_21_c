using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour, IDirectMovable
{
    public float BackgroundLength; // วักขนาดจาก collision แกน x ของ background
    public GameObject[] Background;
    public int BackgroundCount;

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

    // singleton
    public static BackgroundController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    void Update()
    {
        for( int i = 0; i < Background.Length; i++)
        {
            if (Background[i].transform.position.x < -BackgroundLength)
            {
                ReBackground(Background[i]);
            }
        }
        Moving();
    }

    // แกน x เป็น 2 เท่าของขาดรูป , แกน z ถอยเพื่อไม่ให้บังเกมเพลย์
    public void ReBackground(GameObject background)
    {
        Vector3 setPosition = new Vector3(BackgroundLength * BackgroundCount, 0, 1);
        background.transform.position += setPosition;
    }

    public void Moving()
    {
        for ( int i = 0;i < Background.Length;i++)
        {
            Background[i].transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
        }
    }
}
