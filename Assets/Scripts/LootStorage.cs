using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootStorage : ScriptableObject
{
    private GameObject[] _GOStorage;
    private Rigidbody[] _RBStorage;
    private uint _size;
    private uint _lootCount = 0;

    public void SetLoot(GameObject Loot)
    {
        if (Loot.GetComponent<Loot>() == null) return;
        if (_lootCount >= _size) return;

        _GOStorage[_lootCount] = Loot;
        _RBStorage[_lootCount] = Loot.GetComponent<Rigidbody>(); 

        _GOStorage[_lootCount].GetComponent<Loot>().numberInStorage = _lootCount;
        _GOStorage[_lootCount].GetComponent<Loot>().isKeaped = true;

        _lootCount++;
    }
    
    /*public void LoseLoot(uint numberInMassive)
    {
        _GOStorage[numberInMassive].GetComponent<Loot>().isKeaped = false;
        _GOStorage[numberInMassive].GetComponent<Loot>().hand = null;
        _GOStorage[numberInMassive] = null;
        _RBStorage[numberInMassive] = null;
        if (numberInMassive == _lootCount - 1) // если лут последний в массиве
        {
            _GOStorage[numberInMassive] = null;
            _RBStorage[numberInMassive] = null;
            _lootCount--;
        }
        else // если лут в середине массива или первый
        {
            for(uint i = numberInMassive; i < _lootCount; i++)
            {
                _GOStorage[i] = _GOStorage[i + 1];
                _RBStorage[i] = _RBStorage[i + 1];
            }
            _GOStorage[_lootCount - 1] = null;
            _RBStorage[_lootCount - 1] = null;  
            _lootCount--;
        }
        
    }*/

    public GameObject GetLootGO(uint numberInMassive)
    {
        return _GOStorage[numberInMassive];
    }

    public Rigidbody GetLootRB(uint numberInMassive)
    {
        return _RBStorage[numberInMassive];
    }

    public uint GetLootCount()
    {
        return _lootCount;
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
