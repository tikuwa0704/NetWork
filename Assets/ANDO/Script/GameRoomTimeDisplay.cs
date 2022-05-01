using Photon.Pun;
using TMPro;
using UnityEngine;

public class GameRoomTimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI timeLabel;

    private void Start()
    {
        timeLabel = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // �܂����[���ɎQ�����Ă��Ȃ��ꍇ�͍X�V���Ȃ�
        if (!PhotonNetwork.InRoom) { return; }
        // �܂��Q�[���̊J�n�������ݒ肳��Ă��Ȃ��ꍇ�͍X�V���Ȃ�
        if (!PhotonNetwork.CurrentRoom.TryGetStartTime(out int timestamp)) { return; }

        // �Q�[���̌o�ߎ��Ԃ����߂āA�������ʂ܂ŕ\������
        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);
        timeLabel.text = elapsedTime.ToString("f1");
    }
}
