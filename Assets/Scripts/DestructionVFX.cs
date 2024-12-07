using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionVFX : MonoBehaviour
{
    public float DestructionTime;

    private void OnEnable()
    {
        Invoke("Destruction", DestructionTime);
    }
    private void Destruction()
    {
        Destroy(gameObject);
    }

}
