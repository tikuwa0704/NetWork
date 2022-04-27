using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class SimplePun : MonoBehaviourPunCallbacks
{

    // Use this for initialization
    void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        //旧バージョンでは引数必須でしたが、PUN2では不要です。
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        //キャラクターを生成
        GameObject monster = PhotonNetwork.Instantiate("monster", Vector3.zero, Quaternion.identity, 0);
        //自分だけが操作できるようにスクリプトを有効にする
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.enabled = true;

        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("自身がマスタークライアントです");
        }
    }

    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        Debug.Log($"{newPlayer.NickName}が参加しました");
    }

    // 他プレイヤーがルームから退出した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
        Debug.Log($"{otherPlayer.NickName}が退出しました");
    }
}
