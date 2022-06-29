using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerInputController PlayerInputController;
    [HideInInspector] public TimeKeeper timeKeeper;
    private TimeKeeper _timeKeeper;

    private void Start()
    {
        _timeKeeper = GetComponent<TimeKeeper>();
    }

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

    public void StartClock()
    {
        _timeKeeper.enabled = true;
    }

    public void StopClock()
    {
        _timeKeeper.enabled = false;
    }
}
