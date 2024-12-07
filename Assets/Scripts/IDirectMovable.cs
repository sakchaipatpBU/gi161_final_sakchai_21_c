using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirectMovable
{
    public float MoveSpeed { get; set; }
    public void Moving();
}
