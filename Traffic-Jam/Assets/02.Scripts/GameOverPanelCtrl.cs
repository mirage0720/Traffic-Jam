using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanelCtrl : MonoBehaviour
{
    public Button restartButton;

    public void RestartGame()
    {
        // ���� ���� ������ ��ȯ
        SceneManager.LoadScene("GameStartScene");
    }
}