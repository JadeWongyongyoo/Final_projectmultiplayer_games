using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class SpawnObject : NetworkBehaviour
{
    
    public List<GameObject> Spawnobject;
    private void Start()
    {
        if (!IsOwner)
        {
            return;
        }
        InvokeRepeating("Spawn", 1, 2f);
    }
    void Spawn()
    {
        int i = Random.Range(0, Spawnobject.Count-1);
        Vector3 position = new Vector3(Random.Range(-9, 9), 1, 70);
        GameObject Barrier = Instantiate(Spawnobject[i], position, Spawnobject[i].transform.rotation);
        Barrier.GetComponent<NetworkObject>().Spawn();
       
    }
}
