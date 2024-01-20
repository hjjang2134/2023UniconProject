using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    public float BallInitialVelocity = 300f;

    private Rigidbody ballRigidBody = null;

    private bool isBallInPlay = false;  


    // Update is called once per frame
    private void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
    }
        
    void Update()
    {
        // 마우스 오른쪽 키를 누르면 볼에 가속도를 준다
        if(Input.GetButtonDown("Fire1") && !isBallInPlay)
        {
            transform.parent = null;
            isBallInPlay=true;
            ballRigidBody.isKinematic = false;
            ballRigidBody.AddForce(new Vector3(BallInitialVelocity,BallInitialVelocity, 0f));

        }
    }
}
