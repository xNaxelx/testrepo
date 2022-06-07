using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engraver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Loot>() != null)
        {
            if (collision.gameObject.GetComponent<Loot>().stage == 1)
            {
                collision.gameObject.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<Loot>().material[2];
                collision.gameObject.GetComponent<Loot>().stage = 2;
                collision.gameObject.GetComponent<Loot>().cost += 10;
            }
        }
    }
}
