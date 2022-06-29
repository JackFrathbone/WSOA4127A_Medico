using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    public PlayerController playerController;
    public PlayerInputController PlayerInputController;

    public void Pause()
    {
        playerController.disableMovement = true;
        PlayerInputController.disableInput = true;
    }

    public void UnPause()
    {
        playerController.disableMovement = false;
        PlayerInputController.disableInput = false;
    }
}
