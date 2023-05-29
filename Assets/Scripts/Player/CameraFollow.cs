using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // 跟随的目标
    Vector3 camOffset;  // 相机偏移值
    
    // Start is called before the first frame update
    void Start()
    {
        camOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + camOffset;
    }
}
