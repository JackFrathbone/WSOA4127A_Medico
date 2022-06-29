using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerInputController PlayerInputController;
    [HideInInspector] public TimeKeeper timeKeeper;
    [HideInInspector] public ClueManager clueManager;
    private TimeKeeper _timeKeeper;

    private void Start()
    {
        _timeKeeper = GetComponent<TimeKeeper>();
        StartClock();
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
        Time.timeScale = 1;
    }

    public void StopClock()
    {
        _timeKeeper.enabled = false;
        Time.timeScale = 0;
    }

    public bool CheckPaused()
    {
        if(playerController.disableMovement == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckStoppedClock()
    {
        if(_timeKeeper.enabled == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
