using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class UICtrl : MonoBehaviour
{
    public Button leftBtn, rightBtn, quitBtn, startBtn;

    public List<Transform> characterBuffer;

    bool updateUI;

    int currentindex;

    void Awake()
    {
        UpdateUI();
        UpdatePlayer();
        if (quitBtn != null)
        {
            quitBtn.onClick.AddListener(QuitGame);
        }
    }
    void Update()
    {
        if (updateUI)
        {
            updateUI = false;
            UpdateUI();
        }
    }
    void UpdateUI()
    {
        leftBtn.interactable = currentindex > 0;
        rightBtn.interactable = currentindex < characterBuffer.Count - 1;
    }
    void UpdatePlayer()
    {
        for (int i = 0; i < characterBuffer.Count; i++)
        {
            characterBuffer[i].gameObject.SetActive(i == currentindex);
        }
    }
    public void SwitchPlayer(bool LeftOrRight)
    {
        currentindex += LeftOrRight ? -1 : 1;
        updateUI = true;
        UpdatePlayer();
    }
    public void StartScene()
    {
        // ���õ� ĳ������ �ε����� ����
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentindex);
        PlayerPrefs.Save();

        // MainScene���� �̵�
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit(); // ���ø����̼� ����
    }

}