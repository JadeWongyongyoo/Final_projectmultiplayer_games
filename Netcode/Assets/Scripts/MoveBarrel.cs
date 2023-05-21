using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarrel : MonoBehaviour
{
    public float speed = 2f;
    
    void Start()
    {

    }

    void Update()
    {
        float translation_1 = speed;
        transform.position += new Vector3(0, 0, -translation_1);
    }
}
