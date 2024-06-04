using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target; // 플레이어를 따라가는 타겟
    public Vector3 offset;

    private bool isFollowingPlayer = true; // 플레이어를 따라가는지 여부

    void LateUpdate()
    {
        if (isFollowingPlayer && target != null)
        {
            transform.position = target.position + offset;
        }
    }

    // 플레이어가 파괴되었을 때 호출되는 메서드
    public void StopFollowingPlayer()
    {
        isFollowingPlayer = false;
    }
}