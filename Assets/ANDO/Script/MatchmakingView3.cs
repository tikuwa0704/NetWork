using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MatchmakingView3 : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListView roomListView = default;
    [SerializeField]
    private TMP_InputField roomNameInputField = default;
    [SerializeField]
    private Button createRoomButton = default;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // ���r�[�ɎQ������܂ł́A���͂ł��Ȃ��悤�ɂ���
        canvasGroup.interactable = false;

        // ���[�����X�g�\��������������
        roomListView.Init(this);

        roomNameInputField.onValueChanged.AddListener(OnRoomNameInputFieldValueChanged);
        createRoomButton.onClick.AddListener(OnCreateRoomButtonClick);
    }

    public override void OnJoinedLobby()
    {
        // ���r�[�ɎQ��������A���͂ł���悤�ɂ���
        canvasGroup.interactable = true;
    }

    private void OnRoomNameInputFieldValueChanged(string value)
    {
        // ���[������1�����ȏ���͂���Ă��鎞�̂݁A���[���쐬�{�^����������悤�ɂ���
        createRoomButton.interactable = (value.Length > 0);
    }

    private void OnCreateRoomButtonClick()
    {
        Debug.Log("aaa");
        // ���[���쐬�������́A���͂ł��Ȃ��悤�ɂ���
        canvasGroup.interactable = false;
        Debug.Log("bbb");
        // ���̓t�B�[���h�ɓ��͂������[�����̃��[�����쐬����
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
        Debug.Log("ccc");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // ���[���̍쐬�����s������A�Ăѓ��͂ł���悤�ɂ���
        roomNameInputField.text = string.Empty;
        canvasGroup.interactable = true;
    }

    public void OnJoiningRoom()
    {
        // ���[���Q���������́A���͂ł��Ȃ��悤�ɂ���
        canvasGroup.interactable = false;
    }

    public override void OnJoinedRoom()
    {
        // ���[���ւ̎Q��������������AUI���\���ɂ���
        gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ���[���ւ̎Q�������s������A�Ăѓ��͂ł���悤�ɂ���
        canvasGroup.interactable = true;
    }
}
