using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testforce : MonoBehaviour
{
    [SerializeField] public Rigidbody _rg;
    void Start()
    {
        _rg.AddForce(2f, 2f, 0f, ForceMode.VelocityChange);
    }
}
