using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] public Material[] material;
    [SerializeField] public int stage;
    [SerializeField] public float followSpeed = 5;
    [SerializeField] public Rigidbody _rb;
    [HideInInspector] public GameObject Hand = null;
    [HideInInspector] public bool isKeaped = false;
    [HideInInspector] public bool isMoved = false; //костыль. »з-за того что OnCollisionEntered() вызываетс€ у двух объектов одновременно камни одновременно вызывают Hand() и происходит лишнее срабатывание. Ётот флаг нужен дл€ того чтобы OnCollisionEntered() неподобраного камн€ не сработал вместе с подобранным, провер€€ был ли неподобранный камень сдвинут от подобраного
    private float _forceX;
    private Vector3 _forceVector3 = new Vector3();
    private Vector3 _transformBuffer = new Vector3();

    public void Follow(GameObject previousGrabedElement = null)
    {
        if(previousGrabedElement != null)
        {
            _transformBuffer.x = transform.position.x;
            _transformBuffer.y = previousGrabedElement.transform.position.y; ;
            _transformBuffer.z = previousGrabedElement.transform.position.z + 1;
            transform.position = _transformBuffer;

            _forceX = previousGrabedElement.transform.position.x - transform.position.x;
            _forceVector3.x = _forceX * followSpeed;
            _rb.velocity = _forceVector3;
            isMoved = true;
        }
    }

    void Start()
    {
        //gameObject.GetComponent<MeshRenderer>().material = material[1];
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision entered, say: " + gameObject.name + " target: " + collision.gameObject.name + "my cordinates: " + gameObject.transform.position + "target coordinates: " + collision.gameObject.transform.position);
        if ((collision.gameObject.GetComponent<Loot>() != null) & isKeaped & isMoved)
        {
            Debug.Log("colission proofed initiator: " + gameObject.name + " target: " + collision.gameObject.name + " time: " + Time.timeAsDouble + "my cordinates: " + gameObject.transform.position + "target coordinates: " + collision.gameObject.transform.position);
            Hand.GetComponent<Hand>().Grab(collision.rigidbody);
            Debug.Log("after Handling" + " my cordinates: " + gameObject.transform.position + "target coordinates: " + collision.gameObject.transform.position);
        }
    }

    void FixedUpdate()
    {
        if (isKeaped)
        {
            Follow();
        }
    }
}
