using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{

    public GameObject brickParticle;

    //공과의 충ehf할때
    private void OnCollisionEnter(Collision other)
    {
        if (brickParticle != null)
        {
            //이팩트를  발생시킨다
            Instantiate(brickParticle, transform.position, Quaternion.identity);

            GameManager.Instance.DestroyBrick();
            Destroy(gameObject);
        }

    }
}