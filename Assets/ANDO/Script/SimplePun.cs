using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class SimplePun : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject _cameraPrefab;

    // Use this for initialization
    void Start()
    {
        // �v���C���[���g�̖��O��"Player"�ɐݒ肷��
        PhotonNetwork.NickName = "Player";

        //���o�[�W�����ł͈����K�{�ł������APUN2�ł͕s�v�ł��B
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    
    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        PhotonNetwork.JoinLobby();
    }
    
    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
       

        GameObject monster = PhotonNetwork.Instantiate("BasicMotionsDummyModel", Vector3.zero, Quaternion.identity, 0);
        //��������������ł���悤�ɃX�N���v�g��L���ɂ���
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();

        _cameraPrefab.GetComponentInChildren<CameraChange>().ChangeLookAt(monster.transform);
        //�L�����N�^�[�𐶐�
        //GameObject _camera = Instantiate(_cameraPrefab);
        // _camera.GetComponentInChildren<CameraChange>().ChangeLookAt(monster.transform);

        monsterScript.enabled = true;

        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("���g���}�X�^�[�N���C�A���g�ł�");
        }

        // ���[�����쐬�����v���C���[�́A���݂̃T�[�o�[�������Q�[���̊J�n�����ɐݒ肷��
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }
    }

    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        Debug.Log($"{newPlayer.NickName}���Q�����܂���");
    }

    // ���v���C���[�����[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
        Debug.Log($"{otherPlayer.NickName}���ޏo���܂���");
    }
}
