using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] public Material[] material;
    [SerializeField] public float followSpeed = 5;
    [SerializeField] public Rigidbody _rb;
    [HideInInspector] public GameObject hand = null;
    [HideInInspector] public bool isKeaped = false;
    [HideInInspector] public uint stage = 0;
    [HideInInspector] public uint queueNumber;
    [HideInInspector] public uint cost = 10;
    private float _forceX;
    private Vector3 _forceVector3 = new Vector3();
    private Vector3 _transformBuffer = new Vector3();

    public void Follow(GameObject previousGrabedElement)
    {
        _transformBuffer.x = transform.position.x;
        _transformBuffer.y = previousGrabedElement.transform.position.y; ;
        _transformBuffer.z = previousGrabedElement.transform.position.z + 1;
        transform.position = _transformBuffer;

        _forceX = previousGrabedElement.transform.position.x - transform.position.x;
        _forceVector3.x = _forceX * followSpeed;
        _rb.velocity = _forceVector3;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<Loot>() != null) & isKeaped)
        {
            hand.GetComponent<Hand>().Grab(collision.rigidbody);
        }
    }

}
