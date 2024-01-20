using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    private Transform tr;



    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        tr.position = new Vector3(target.position.x, tr.position.y, tr.position.z);

        tr.LookAt(target);
    }
}
