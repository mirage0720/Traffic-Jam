using System.Collections;
using UnityEngine;

public class ItemPrefabSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Item 프리팹들을 저장할 배열
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // 소환할 위치의 영역 크기
    public float itemspawnInterval = 5f; // 아이템 생성 간격 (초)
    private bool spawningEnabled = true; // 생성 활성화 여부

    void Start()
    {
        // 일정 시간마다 SpawnItems 코루틴 호출
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (spawningEnabled)
        {
            // 랜덤한 위치에서 아이템 생성
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject itemPrefab = GetRandomItemPrefab();
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

            // itemspawnInterval 시간 동안 대기
            yield return new WaitForSeconds(itemspawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 계산
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            0f,
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        ) + transform.position; // 스폰 위치를 이동한 위치로 설정
        return spawnPosition;
    }

    GameObject GetRandomItemPrefab()
    {
        // 랜덤한 Item 프리팹 선택
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        return itemPrefabs[randomIndex];
    }

    public void StopSpawning()
    {
        spawningEnabled = false;
    }

    // 에디터에서 스폰 영역을 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

}