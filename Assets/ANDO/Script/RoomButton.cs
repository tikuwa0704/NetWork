using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    private const int MaxPlayers = 4;

    [SerializeField]
    private TextMeshProUGUI label = default;

    private MatchmakingView2 matchmakingView;
    private Button button;

    public string RoomName { get; private set; }

    public void Init(MatchmakingView2 parentView, int roomId)
    {
        matchmakingView = parentView;
        RoomName = $"Room{roomId}";

        button = GetComponent<Button>();
        button.interactable = false;
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // ���[���Q���������́A�S�Ă̎Q���{�^���������Ȃ��悤�ɂ���
        matchmakingView.OnJoiningRoom();

        // �{�^���ɑΉ��������[�����̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���Ă���Q������j
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

    public void SetPlayerCount(int playerCount)
    {
        label.text = $"{RoomName}\n{playerCount} / {MaxPlayers}";

        // ���[���������łȂ����̂݁A���[���Q���{�^����������悤�ɂ���
        button.interactable = (playerCount < MaxPlayers);
    }
}
