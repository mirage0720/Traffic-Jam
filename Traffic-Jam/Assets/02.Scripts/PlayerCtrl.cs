using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public int level = 1; // Player는 항상 1레벨로 시작
    public float speed;
    public float jumpForce;
    public VariableJoystick joy;
    private bool isGrounded;
    Rigidbody rb;
    Vector3 moveVec;

    public GameObject gameOverPanel; // 게임 오버 패널 참조
    public Text levelText; // 현재 레벨을 표시할 텍스트

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Player"; // Player 태그 설정
    }

    void FixedUpdate()
    {
        // Input Value
        float x = joy.Horizontal;
        float z = joy.Vertical;

        // Move Position
        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
        {
            return; // No Input = No Rotation
        }

        // Move Rotation
        Quaternion lookRotation = Quaternion.LookRotation(moveVec.normalized);
        rb.rotation = lookRotation;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // 점프하면 땅에 닿아있는 상태가 아님
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿으면 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            EnemyCtrl enemy = collision.gameObject.GetComponent<EnemyCtrl>();
            if (enemy != null)
            {
                HandleCollision(enemy.level, collision.gameObject);
            }
        }
    }

    void HandleCollision(int otherLevel, GameObject otherObject)
    {
        // 레벨 비교
        if (level > otherLevel)
        {
            int gainedLevels = otherLevel;
            level += gainedLevels; // 상대방의 레벨만큼 레벨업
            Debug.Log($"Player leveled up by {gainedLevels} to level {level}");
            Destroy(otherObject); // 상대방이 레벨이 낮으면 파괴
            CheckAndAdjustScale(); // 레벨업 후 스케일 조정 확인
        }
        else if (level < otherLevel)
        {
            Destroy(gameObject); // 자신의 레벨이 낮으면 파괴
            EndGame(); // 게임 종료 처리 함수 호출
        }
        // 레벨이 같으면 아무 일도 일어나지 않음
    }

    public void CheckAndAdjustScale()
    {
        // 스케일이 최소 1 이상이 되도록 설정
        float scaleIncrease = (level / 100f) * 0.1f;
        float newScale = Mathf.Max(1f, transform.localScale.x + scaleIncrease);
        transform.localScale = new Vector3(newScale, newScale, newScale);
        Debug.Log($"Enemy {gameObject.name} scaled to {newScale}x due to level {level}");
    }

    void EndGame()
    {
        // 적 스포너 정지
        EnemyPrefabSpawner[] enemySpawners = FindObjectsOfType<EnemyPrefabSpawner>();
        foreach (EnemyPrefabSpawner spawner in enemySpawners)
        {
            spawner.StopSpawning();
        }

        // 아이템 스포너 정지
        ItemPrefabSpawner[] itemSpawners = FindObjectsOfType<ItemPrefabSpawner>();
        foreach (ItemPrefabSpawner spawner in itemSpawners)
        {
            spawner.StopSpawning();
        }

        // 게임 오버 패널 활성화
        gameOverPanel.SetActive(true);
        levelText.text = "Your Level: " + level;
    }
}