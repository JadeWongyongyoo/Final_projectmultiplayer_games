using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Destroy_object : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {

        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            DestroyServerRpc();
        }
        else if (other.gameObject.tag == "DeathZone") 
        {
            Debug.Log("+1");
            Debug.Log("Death hit");
            DestroyServerRpc();
        }
        
    } 
}

