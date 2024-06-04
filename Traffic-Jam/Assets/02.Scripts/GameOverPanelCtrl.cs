using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanelCtrl : MonoBehaviour
{
    public Button restartButton;

    public void RestartGame()
    {
        // 게임 시작 씬으로 전환
        SceneManager.LoadScene("GameStartScene");
    }
}