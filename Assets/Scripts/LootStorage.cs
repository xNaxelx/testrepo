using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootStorage : MonoBehaviour
{
    private GameObject[] _GOStorage;
    private Rigidbody[] _RBStorage;
    private Loot[] _lootStorage;
    private uint _size;
    private uint _lootCount = 0;

    public void SetLoot(GameObject Loot)
    {
        if (Loot.GetComponent<Loot>() == null) return;
        if (_lootCount >= _size) return;

        _GOStorage[_lootCount] = Loot;
        _RBStorage[_lootCount] = Loot.GetComponent<Rigidbody>();
        _lootStorage[_lootCount] = Loot.GetComponent<Loot>();
        _lootCount++;

        Loot.GetComponent<Loot>().numberInStorage = _lootCount - 1;
    }

    public void LoosLoot(uint numberInMassive)
    {
        _GOStorage[numberInMassive] = null;
        _RBStorage[numberInMassive] = null;
        _lootStorage[numberInMassive] = null;
        _lootCount--;
    }

    public GameObject GetLootGO(uint numberInMassive)
    {
        return _GOStorage[numberInMassive];
    }

    public Rigidbody GetLootRB(uint numberInMassive)
    {
        return _RBStorage[numberInMassive];
    }

    public Loot GetLootScript(uint numberInMassive)
    {
        return _lootStorage[numberInMassive];
    }

    public LootStorage(uint size)
    {
        _GOStorage = new GameObject[size];
        _RBStorage = new Rigidbody[size];
        _size = size;
    }

    ~LootStorage()
    {

    }
}
