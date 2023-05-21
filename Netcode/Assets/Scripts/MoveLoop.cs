using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MoveLoop : MonoBehaviour
{
    public float speed = 0.01f;
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
        transform.position += new Vector3(0, -translation_1, 0);
        if (transform.position.y < -5)
        {
            transform.position = StartPosition;
        }
    }
}
