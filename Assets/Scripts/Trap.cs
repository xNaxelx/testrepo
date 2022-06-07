using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Hand _hand;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Loot>() != null)
        {
            if (collision.gameObject.GetComponent<Loot>().isKeaped == true)
            {
                _hand = collision.gameObject.GetComponent<Loot>().hand.GetComponent<Hand>();
                for(uint i = 0; i < _hand.lootCount; i++)
                {
                    if(_hand.lootStorage[i] == collision.rigidbody)
                    {
                        Debug.Log("if start");
                        for (uint o = 0; i + o < _hand.lootCount; o++)
                        {
                            Destroy(_hand.lootStorage[i + o].gameObject);
                            _hand.lootStorage[i + o] = null; 
                            Debug.Log("destroy and null");
                        }
                        Debug.Log("for end");
                        _hand.lootCount = i + 1; 
                        Debug.Log(i);
                        Debug.Log("if end");
                    }
                }
            }
        }
    }
}
