using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private Vector3 origin; // 弾を発射した時刻の座標
    private Vector3 velocity;
    private int timestamp;
    
    [SerializeField] float speed;

    public int Id { get; private set; }
    // 弾を発射したプレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }
    // 同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;


    public void Init(int id, int ownerId,Vector3 origin, Vector3 forward,int timestamp)
    {
        Id = id;
        OwnerId = ownerId;
        this.origin = origin;
        //transform.position = origin;
        velocity = forward.normalized;
        transform.rotation = Quaternion.LookRotation(forward);
        this.timestamp = timestamp;

        Update();
    }

    private void Update()
    {
        //transform.Translate(new Vector3(0,0,speed) * Time.deltaTime);
        // 弾を発射した時刻から現在時刻までの経過時間を求める
        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);
        // 弾を発射した時刻での座標・速度・経過時間から現在の座標を求める
        transform.position = origin + velocity * speed * elapsedTime;
    }


    // 画面外に移動したら削除する
    // （Unityのエディター上ではシーンビューの画面も影響するので注意）
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
