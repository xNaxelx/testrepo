using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public uint lootCount = 0;
    public bool isFinished = false;
    private Input_Controls _input;
    private Camera _camera;
    private Vector3 _screenMousePosition = new Vector3();
    private Vector3 _InGameMousePosition = new Vector3();
    [SerializeField] public Rigidbody[] lootStorage = new Rigidbody[1000];

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
        for(int i = 0; i < lootCount; i++)
        {
            if(Loot == lootStorage[i])
            {
                return;
            }
        }
        lootStorage[lootCount] = Loot;

        Loot.GetComponent<Loot>().isKeaped = true;
        Loot.GetComponent<Loot>().hand = gameObject;
        Loot.GetComponent<Loot>().numberInStorage = lootCount;
        lootCount++;
    }

    private void MoveStone()
    {
        for(int i = 0; i < lootCount; i++)
        {
            if(i == 0)
            {
                lootStorage[i].gameObject.GetComponent<Loot>().Follow(gameObject);
            }
            else
            {
                lootStorage[i].gameObject.GetComponent<Loot>().Follow(lootStorage[i - 1].gameObject);
            }
        }
    }

    public void Kostil(ref uint lootCount)
    {
        lootCount--;
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
        if(isFinished == false)
        {
            MoveHand();
        }
        else
        {
            _InGameMousePosition.x = 0;
            _InGameMousePosition.z = gameObject.transform.position.z; // если этой строки не будет, рука зависает в одном месте
            gameObject.transform.position = _InGameMousePosition;
        }
        MoveStone();
    }
}
