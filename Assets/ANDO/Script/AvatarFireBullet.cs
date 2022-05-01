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
            // 左クリックでカーソルの方向に弾を発射する
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector3 forward = this.transform.forward; 

                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++, forward);
            }
        }
    }

    
    // 弾を発射するメソッド
    [PunRPC]
    private void FireBullet(int id,Vector3 forward, PhotonMessageInfo info)
    {
        var bullet = Instantiate(bulletPrefab);
        // PhotonMessageInfoから、RPCを送信した時刻を取得する
        // 弾を発射した時刻に50msのディレイをかける
        int timestamp = unchecked(info.SentServerTimestamp + 50);
        bullet.Init(id, photonView.OwnerActorNr, transform.position + new Vector3(0,1.5f,0), forward, timestamp);
    }
}
