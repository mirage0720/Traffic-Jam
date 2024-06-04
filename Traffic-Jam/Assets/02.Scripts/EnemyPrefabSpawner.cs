using System.Collections;
using UnityEngine;

public class EnemyPrefabSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Enemy �����յ��� ������ �迭
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // ��ȯ�� ��ġ�� ���� ũ��
    public float spawnInterval = 2f; // �� ���� ���� (��)
    private bool spawningEnabled = true; // ���� Ȱ��ȭ ����

    void Start()
    {
        // ���� �ð����� SpawnEnemies �ڷ�ƾ ȣ��
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawningEnabled)
        {
            // ������ ��ġ���� �� ����
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // spawnInterval �ð� ���� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // ������ ��ġ ���
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            0f,
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        ) + transform.position; // ���� ��ġ�� �̵��� ��ġ�� ����
        return spawnPosition;
    }

    GameObject GetRandomEnemyPrefab()
    {
        // ������ Enemy ������ ����
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    public void StopSpawning()
    {
        spawningEnabled = false;
    }

    // �����Ϳ��� ���� ������ �ð�ȭ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}