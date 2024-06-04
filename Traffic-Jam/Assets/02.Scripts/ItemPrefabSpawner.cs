using System.Collections;
using UnityEngine;

public class ItemPrefabSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Item �����յ��� ������ �迭
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // ��ȯ�� ��ġ�� ���� ũ��
    public float itemspawnInterval = 5f; // ������ ���� ���� (��)
    private bool spawningEnabled = true; // ���� Ȱ��ȭ ����

    void Start()
    {
        // ���� �ð����� SpawnItems �ڷ�ƾ ȣ��
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (spawningEnabled)
        {
            // ������ ��ġ���� ������ ����
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject itemPrefab = GetRandomItemPrefab();
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

            // itemspawnInterval �ð� ���� ���
            yield return new WaitForSeconds(itemspawnInterval);
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

    GameObject GetRandomItemPrefab()
    {
        // ������ Item ������ ����
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        return itemPrefabs[randomIndex];
    }

    public void StopSpawning()
    {
        spawningEnabled = false;
    }

    // �����Ϳ��� ���� ������ �ð�ȭ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

}