using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed = 5f;
    public Transform _rotator;
    
    // Start is called before the first frame update
    void Start()
    {
        _rotator = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _rotator.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
