using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public List<GameObject> characterPrefabs; // ĳ���� �������� ������ ����Ʈ
    public Transform spawnPoint; // ĳ���͸� ��ȯ�� ��ġ
    public Transform playerSpawnParent; // PlayerSpawn ��ü

    private int selectedCharacterIndex; // ���õ� ĳ������ �ε���

    void Start()
    {
        // UICtrl���� ���õ� ĳ������ �ε����� ������
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

        // ���õ� ĳ���͸� ��ȯ
        SpawnSelectedCharacter();
    }

    void SpawnSelectedCharacter()
    {
        // �ε����� ��ȿ���� Ȯ��
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Count)
        {
            // ���õ� ĳ���͸� ��ȯ
            GameObject selectedCharacterPrefab = characterPrefabs[selectedCharacterIndex];
            GameObject characterInstance = Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);

            // PlayerSpawn ��ü�� �ڽ����� ����
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