using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionControl : MonoBehaviour
{
    private void OnCollisionStay(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }
}
