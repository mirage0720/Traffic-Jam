using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target; // �÷��̾ ���󰡴� Ÿ��
    public Vector3 offset;

    private bool isFollowingPlayer = true; // �÷��̾ ���󰡴��� ����

    void LateUpdate()
    {
        if (isFollowingPlayer && target != null)
        {
            transform.position = target.position + offset;
        }
    }

    // �÷��̾ �ı��Ǿ��� �� ȣ��Ǵ� �޼���
    public void StopFollowingPlayer()
    {
        isFollowingPlayer = false;
    }
}