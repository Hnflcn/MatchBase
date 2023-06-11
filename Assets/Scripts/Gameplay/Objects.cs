using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public Vector2 myPos;
    public Fruits myName;

   private void Start()
    {
        myPos = transform.position;
    }

}
