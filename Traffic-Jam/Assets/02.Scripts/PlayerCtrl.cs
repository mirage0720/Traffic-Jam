using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public int level = 1; // Player�� �׻� 1������ ����
    public float speed;
    public float jumpForce;
    public VariableJoystick joy;
    private bool isGrounded;
    Rigidbody rb;
    Vector3 moveVec;

    public GameObject gameOverPanel; // ���� ���� �г� ����
    public Text levelText; // ���� ������ ǥ���� �ؽ�Ʈ

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Player"; // Player �±� ����
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
            isGrounded = false; // �����ϸ� ���� ����ִ� ���°� �ƴ�
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ������ isGrounded�� true�� ����
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
        // ���� ��
        if (level > otherLevel)
        {
            int gainedLevels = otherLevel;
            level += gainedLevels; // ������ ������ŭ ������
            Debug.Log($"Player leveled up by {gainedLevels} to level {level}");
            Destroy(otherObject); // ������ ������ ������ �ı�
            CheckAndAdjustScale(); // ������ �� ������ ���� Ȯ��
        }
        else if (level < otherLevel)
        {
            Destroy(gameObject); // �ڽ��� ������ ������ �ı�
            EndGame(); // ���� ���� ó�� �Լ� ȣ��
        }
        // ������ ������ �ƹ� �ϵ� �Ͼ�� ����
    }

    public void CheckAndAdjustScale()
    {
        // �������� �ּ� 1 �̻��� �ǵ��� ����
        float scaleIncrease = (level / 100f) * 0.1f;
        float newScale = Mathf.Max(1f, transform.localScale.x + scaleIncrease);
        transform.localScale = new Vector3(newScale, newScale, newScale);
        Debug.Log($"Enemy {gameObject.name} scaled to {newScale}x due to level {level}");
    }

    void EndGame()
    {
        // �� ������ ����
        EnemyPrefabSpawner[] enemySpawners = FindObjectsOfType<EnemyPrefabSpawner>();
        foreach (EnemyPrefabSpawner spawner in enemySpawners)
        {
            spawner.StopSpawning();
        }

        // ������ ������ ����
        ItemPrefabSpawner[] itemSpawners = FindObjectsOfType<ItemPrefabSpawner>();
        foreach (ItemPrefabSpawner spawner in itemSpawners)
        {
            spawner.StopSpawning();
        }

        // ���� ���� �г� Ȱ��ȭ
        gameOverPanel.SetActive(true);
        levelText.text = "Your Level: " + level;
    }
}