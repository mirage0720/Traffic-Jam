using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public enum OperationType { Add, Subtract, Multiply, Divide } // ��Ģ���� ����
    public OperationType operation; // ���� Ÿ��
    public int value; // ���꿡 ���� ��

    void Start()
    {
        // Ȯ�� ������� ���� Ÿ�� ����
        float rand = Random.value;
        if (rand < 0.1f)
        {
            operation = OperationType.Multiply; // 10% Ȯ���� ���ϱ�
        }
        else if (rand < 0.4f)
        {
            operation = OperationType.Add; // 30% Ȯ���� ���ϱ�
        }
        else if (rand < 0.7f)
        {
            operation = OperationType.Subtract; // 30% Ȯ���� ����
        }
        else
        {
            operation = OperationType.Divide; // 30% Ȯ���� ������
        }

        value = Random.Range(1, 4); // 1���� 3���� ���� ��

        // Collider�� Ʈ���ŷ� ����
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Ground���� �浹 ����
        if (other.CompareTag("Ground"))
        {
            return;
        }

        // Player�� �浹���� ��
        PlayerCtrl player = other.GetComponent<PlayerCtrl>();
        if (player != null)
        {
            ApplyOperation(player);
            Destroy(gameObject); // ������ ��� �� �ı�
            return;
        }

        // Enemy�� �浹���� ��
        EnemyCtrl enemy = other.GetComponent<EnemyCtrl>();
        if (enemy != null)
        {
            ApplyOperation(enemy);
            Destroy(gameObject); // ������ ��� �� �ı�
        }
    }

    void ApplyOperation(PlayerCtrl player)
    {
        // �÷��̾��� ������ ��Ģ���� ����
        switch (operation)
        {
            case OperationType.Add:
                player.level += value;
                break;
            case OperationType.Subtract:
                player.level = Mathf.Max(1, player.level - value); // ������ 1 ���Ϸ� �������� �ʵ��� ��
                break;
            case OperationType.Multiply:
                player.level *= value;
                break;
            case OperationType.Divide:
                player.level = Mathf.Max(1, player.level / value); // ������ 1 ���Ϸ� �������� �ʵ��� ��
                break;
        }
        player.CheckAndAdjustScale(); // ������ ����
    }

    void ApplyOperation(EnemyCtrl enemy)
    {
        // ���� ������ ��Ģ���� ����
        switch (operation)
        {
            case OperationType.Add:
                enemy.level += value;
                break;
            case OperationType.Subtract:
                enemy.level = Mathf.Max(1, enemy.level - value); // ������ 1 ���Ϸ� �������� �ʵ��� ��
                break;
            case OperationType.Multiply:
                enemy.level *= value;
                break;
            case OperationType.Divide:
                enemy.level = Mathf.Max(1, enemy.level / value); // ������ 1 ���Ϸ� �������� �ʵ��� ��
                break;
        }
        enemy.CheckAndAdjustScale(); // ������ ����
    }
}