using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    public int level; // Enemy의 레벨
    NavMeshAgent navMeshAgent;
    Vector3 targetPosition;
    bool isColliding; // 충돌 여부

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        level = Random.Range(1, 11); // 랜덤한 레벨 값 설정 (예: 1부터 10까지)
        gameObject.tag = "Enemy"; // Enemy 태그 설정
        SetRandomDestination();
    }

    void Update()
    {
        // 충돌이 일어나지 않을 때만 이동
        if (!isColliding)
        {
            // Enemy가 목표 지점에 도착하면 다시 새로운 목표 지점을 설정
            if (navMeshAgent.remainingDistance < 0.5f)
            {
                SetRandomDestination();
            }
        }
    }

    void SetRandomDestination()
    {
        // 맵 안에서 랜덤한 위치로 이동
        targetPosition = GetRandomPositionInMapBounds();
        navMeshAgent.SetDestination(targetPosition);
    }

    Vector3 GetRandomPositionInMapBounds()
    {
        // 맵 경계를 벗어나지 않는 랜덤한 위치 계산
        float mapWidth = 50f; // 맵의 가로 길이
        float mapHeight = 50f; // 맵의 세로 길이

        float randomX = Random.Range(-mapWidth / 2f, mapWidth / 2f);
        float randomZ = Random.Range(-mapHeight / 2f, mapHeight / 2f);

        return new Vector3(randomX, 0f, randomZ);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 다른 Enemy와 충돌했을 때
        EnemyCtrl otherEnemy = collision.gameObject.GetComponent<EnemyCtrl>();
        if (otherEnemy != null)
        {
            HandleCollision(otherEnemy.level, otherEnemy.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCtrl player = collision.gameObject.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                HandleCollision(player.level, collision.gameObject);
            }
        }

        isColliding = true; // 충돌 상태 설정
        // 충돌 후 잠시동안 대기하고 다시 움직임
        Invoke("ResetCollisionState", 0.2f);
    }
    void ResetCollisionState()
    {
        isColliding = false; // 충돌 상태 초기화
        SetRandomDestination(); // 랜덤한 경로 설정
    }

    public void HandleCollision(int otherLevel, GameObject otherObject)
    {
        // 레벨 비교
        if (level > otherLevel)
        {
            int gainedLevels = otherLevel;
            level += gainedLevels; // 상대방의 레벨만큼 레벨업
            Destroy(otherObject); // 상대방이 레벨이 낮으면 파괴
            CheckAndAdjustScale(); // 레벨업 후 스케일 조정 확인
        }
        else if (level < otherLevel)
        {
            Destroy(gameObject); // 자신의 레벨이 낮으면 파괴
        }
        // 레벨이 같으면 아무 일도 일어나지 않음
    }

    public void CheckAndAdjustScale()
    {
        // 스케일이 최소 1 이상이 되도록 설정
        float scaleIncrease = (level / 100f) * 0.1f;
        float newScale = Mathf.Max(1f, transform.localScale.x + scaleIncrease);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}