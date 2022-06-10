using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engraver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Loot>() != null)
        {
            collision.gameObject.GetComponent<Loot>().IncreaseStage(1);
        }
    }
}
