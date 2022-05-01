using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarFireBullet : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Bullet bulletPrefab = default;

    private int nextBulletId = 0;

    private void Update()
    {
        if (photonView.IsMine)
        {
            // ���N���b�N�ŃJ�[�\���̕����ɒe�𔭎˂���
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector3 forward = this.transform.forward; 

                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++, forward);
            }
        }
    }

    
    // �e�𔭎˂��郁�\�b�h
    [PunRPC]
    private void FireBullet(int id,Vector3 forward, PhotonMessageInfo info)
    {
        var bullet = Instantiate(bulletPrefab);
        // PhotonMessageInfo����ARPC�𑗐M�����������擾����
        // �e�𔭎˂���������50ms�̃f�B���C��������
        int timestamp = unchecked(info.SentServerTimestamp + 50);
        bullet.Init(id, photonView.OwnerActorNr, transform.position + new Vector3(0,1.5f,0), forward, timestamp);
    }
}
