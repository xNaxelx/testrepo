using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Input_Controls _input;
    private Camera _camera;
    private Vector3 _screenMousePosition = new Vector3();
    private Vector3 _InGameMousePosition = new Vector3();
    [SerializeField]private uint _lootCount = 0;
    private Rigidbody[] _lootStorage = new Rigidbody[1000];

    private void MoveHand()
    {
        _screenMousePosition = _input.Action_Map.TapPosition.ReadValue<Vector2>();
        _screenMousePosition.z = 7; //расстояние от камеры до руки, пока хардкод, но нужно исправить
        _InGameMousePosition = _camera.ScreenToWorldPoint(_screenMousePosition);

        _InGameMousePosition.y = gameObject.transform.position.y;
        _InGameMousePosition.z = gameObject.transform.position.z;

        gameObject.transform.position = _InGameMousePosition;
    }

    public void Grab(Rigidbody Loot)
    {
        _lootStorage[_lootCount] = Loot;
        if(_lootCount == 0)
        {
            Loot.GetComponent<Loot>().previousGrabedElement = gameObject;
        }
        else
        {
            Loot.GetComponent<Loot>().previousGrabedElement = _lootStorage[_lootCount - 1].gameObject;
        }
        Loot.GetComponent<Loot>().Hand = gameObject;
        Loot.GetComponent<Loot>().isKeaped = true;
        _lootCount++;

    }

    private void Awake()
    {
        _input = new Input_Controls();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Loot>() != null)
        {
            Grab(collision.rigidbody);
        }
    }

    void FixedUpdate()
    {
        MoveHand(); 
    }
}
