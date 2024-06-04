using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
    public Button jumpButton;
    public PlayerCtrl playerCtrl;

    void Start()
    {
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(OnJumpButtonPressed);
        }
        else
        {
            Debug.LogError("Jump Button is not assigned in the Inspector");
        }

        if (playerCtrl == null)
        {
            playerCtrl = FindObjectOfType<PlayerCtrl>();
        }
    }

    void OnJumpButtonPressed()
    {
        if (playerCtrl != null)
        {
            playerCtrl.Jump();
        }
        else
        {
            Debug.LogError("PlayerCtrl is not assigned or found");
        }
    }
}