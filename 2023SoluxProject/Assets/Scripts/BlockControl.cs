using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{

    public GameObject brickParticle;

    //������ ��ehf�Ҷ�
    private void OnCollisionEnter(Collision other)
    {
        if (brickParticle != null)
        {
            //����Ʈ��  �߻���Ų��
            Instantiate(brickParticle, transform.position, Quaternion.identity);

            GameManager.Instance.DestroyBrick();
            Destroy(gameObject);
        }

    }
}