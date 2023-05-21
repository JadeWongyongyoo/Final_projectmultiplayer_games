using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BGScroll : NetworkBehaviour
{
    public float speed = 4f;
    private Vector3 StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float translation_1 = speed;
        transform.position += new Vector3(0, 0, -translation_1);       
        if(transform.position.z < -80)
        {
            transform.position=StartPosition;
        }
    }
}
