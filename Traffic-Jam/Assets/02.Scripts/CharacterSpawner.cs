using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public List<GameObject> characterPrefabs; // 캐릭터 프리팹을 저장할 리스트
    public Transform spawnPoint; // 캐릭터를 소환할 위치
    public Transform playerSpawnParent; // PlayerSpawn 객체

    private int selectedCharacterIndex; // 선택된 캐릭터의 인덱스

    void Start()
    {
        // UICtrl에서 선택된 캐릭터의 인덱스를 가져옴
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

        // 선택된 캐릭터를 소환
        SpawnSelectedCharacter();
    }

    void SpawnSelectedCharacter()
    {
        // 인덱스가 유효한지 확인
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Count)
        {
            // 선택된 캐릭터를 소환
            GameObject selectedCharacterPrefab = characterPrefabs[selectedCharacterIndex];
            GameObject characterInstance = Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);

            // PlayerSpawn 객체의 자식으로 설정
            if (playerSpawnParent != null)
            {
                characterInstance.transform.SetParent(playerSpawnParent, false);
            }
        }
        else
        {
            Debug.LogWarning("Invalid selected character index!");
        }
    }
}