using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public enum OperationType { Add, Subtract, Multiply, Divide } // 사칙연산 종류
    public OperationType operation; // 연산 타입
    public int value; // 연산에 사용될 값

    void Start()
    {
        // 확률 기반으로 연산 타입 선택
        float rand = Random.value;
        if (rand < 0.1f)
        {
            operation = OperationType.Multiply; // 10% 확률로 곱하기
        }
        else if (rand < 0.4f)
        {
            operation = OperationType.Add; // 30% 확률로 더하기
        }
        else if (rand < 0.7f)
        {
            operation = OperationType.Subtract; // 30% 확률로 빼기
        }
        else
        {
            operation = OperationType.Divide; // 30% 확률로 나누기
        }

        value = Random.Range(1, 4); // 1부터 3까지 랜덤 값

        // Collider를 트리거로 설정
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Ground와의 충돌 무시
        if (other.CompareTag("Ground"))
        {
            return;
        }

        // Player와 충돌했을 때
        PlayerCtrl player = other.GetComponent<PlayerCtrl>();
        if (player != null)
        {
            ApplyOperation(player);
            Destroy(gameObject); // 아이템 사용 후 파괴
            return;
        }

        // Enemy와 충돌했을 때
        EnemyCtrl enemy = other.GetComponent<EnemyCtrl>();
        if (enemy != null)
        {
            ApplyOperation(enemy);
            Destroy(gameObject); // 아이템 사용 후 파괴
        }
    }

    void ApplyOperation(PlayerCtrl player)
    {
        // 플레이어의 레벨에 사칙연산 적용
        switch (operation)
        {
            case OperationType.Add:
                player.level += value;
                break;
            case OperationType.Subtract:
                player.level = Mathf.Max(1, player.level - value); // 레벨이 1 이하로 내려가지 않도록 함
                break;
            case OperationType.Multiply:
                player.level *= value;
                break;
            case OperationType.Divide:
                player.level = Mathf.Max(1, player.level / value); // 레벨이 1 이하로 내려가지 않도록 함
                break;
        }
        player.CheckAndAdjustScale(); // 스케일 조정
    }

    void ApplyOperation(EnemyCtrl enemy)
    {
        // 적의 레벨에 사칙연산 적용
        switch (operation)
        {
            case OperationType.Add:
                enemy.level += value;
                break;
            case OperationType.Subtract:
                enemy.level = Mathf.Max(1, enemy.level - value); // 레벨이 1 이하로 내려가지 않도록 함
                break;
            case OperationType.Multiply:
                enemy.level *= value;
                break;
            case OperationType.Divide:
                enemy.level = Mathf.Max(1, enemy.level / value); // 레벨이 1 이하로 내려가지 않도록 함
                break;
        }
        enemy.CheckAndAdjustScale(); // 스케일 조정
    }
}