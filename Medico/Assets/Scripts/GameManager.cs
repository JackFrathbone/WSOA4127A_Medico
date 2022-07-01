using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    private int _defaultScene = 1;

    [Header("References")]
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerInputController PlayerInputController;
    [HideInInspector] public PlayerHealingController playerHealingController;
    [HideInInspector] public PlayerInventory playerInventory;
    [HideInInspector] public TimeKeeper timeKeeper;
    [HideInInspector] public ClueManager clueManager;

    private TimeKeeper _timeKeeper;

    private void Start()
    {
        _timeKeeper = GetComponent<TimeKeeper>();
        Pause();
        StopClock();

        LoadLocation(_defaultScene);
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

    public void LoadLocation(int i)
    {
        SceneManager.LoadScene(i, LoadSceneMode.Additive);
    }

    public void UnloadLocation(int i)
    {
        SceneManager.UnloadSceneAsync(i);
        Resources.UnloadUnusedAssets();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
