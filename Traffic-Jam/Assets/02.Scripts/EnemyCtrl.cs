using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    public int level; // Enemy�� ����
    NavMeshAgent navMeshAgent;
    Vector3 targetPosition;
    bool isColliding; // �浹 ����

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        level = Random.Range(1, 11); // ������ ���� �� ���� (��: 1���� 10����)
        gameObject.tag = "Enemy"; // Enemy �±� ����
        SetRandomDestination();
    }

    void Update()
    {
        // �浹�� �Ͼ�� ���� ���� �̵�
        if (!isColliding)
        {
            // Enemy�� ��ǥ ������ �����ϸ� �ٽ� ���ο� ��ǥ ������ ����
            if (navMeshAgent.remainingDistance < 0.5f)
            {
                SetRandomDestination();
            }
        }
    }

    void SetRandomDestination()
    {
        // �� �ȿ��� ������ ��ġ�� �̵�
        targetPosition = GetRandomPositionInMapBounds();
        navMeshAgent.SetDestination(targetPosition);
    }

    Vector3 GetRandomPositionInMapBounds()
    {
        // �� ��踦 ����� �ʴ� ������ ��ġ ���
        float mapWidth = 50f; // ���� ���� ����
        float mapHeight = 50f; // ���� ���� ����

        float randomX = Random.Range(-mapWidth / 2f, mapWidth / 2f);
        float randomZ = Random.Range(-mapHeight / 2f, mapHeight / 2f);

        return new Vector3(randomX, 0f, randomZ);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �ٸ� Enemy�� �浹���� ��
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

        isColliding = true; // �浹 ���� ����
        // �浹 �� ��õ��� ����ϰ� �ٽ� ������
        Invoke("ResetCollisionState", 0.2f);
    }
    void ResetCollisionState()
    {
        isColliding = false; // �浹 ���� �ʱ�ȭ
        SetRandomDestination(); // ������ ��� ����
    }

    public void HandleCollision(int otherLevel, GameObject otherObject)
    {
        // ���� ��
        if (level > otherLevel)
        {
            int gainedLevels = otherLevel;
            level += gainedLevels; // ������ ������ŭ ������
            Destroy(otherObject); // ������ ������ ������ �ı�
            CheckAndAdjustScale(); // ������ �� ������ ���� Ȯ��
        }
        else if (level < otherLevel)
        {
            Destroy(gameObject); // �ڽ��� ������ ������ �ı�
        }
        // ������ ������ �ƹ� �ϵ� �Ͼ�� ����
    }

    public void CheckAndAdjustScale()
    {
        // �������� �ּ� 1 �̻��� �ǵ��� ����
        float scaleIncrease = (level / 100f) * 0.1f;
        float newScale = Mathf.Max(1f, transform.localScale.x + scaleIncrease);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}