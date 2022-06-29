using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("Settings")]
    //How far from the player the target can be activated
    [SerializeField] float _clickDistanceLimit;
    public bool disableInput;
    private bool _enablePauseMenu;
    private bool _pauseMenuPaused;
    private bool _pauseMenuStoppedClock;

    [Header("References")]
    [SerializeField] GameObject _pauseMenu;
    private PlayerController _playerController;
    private DialogueController _dialogueController;

    private void Awake()
    {
        GameManager.Instance.PlayerInputController = this;
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _dialogueController = GetComponent<DialogueController>();

        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (!disableInput)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            float mouseDistance = Vector3.Distance(mousePos, _playerController.transform.position);

            //On hover
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                if (hit.collider != null && mouseDistance <= _clickDistanceLimit)
                {
          
                }
            }

            //Left click
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null && mouseDistance <= _clickDistanceLimit)
                {
                    if (hit.collider.CompareTag("NPC"))
                    {
                        NPCManager npc = hit.collider.GetComponent<NPCManager>();
                        if(npc.npcState == NPCManager.NPCState.alive)
                        {
                            _dialogueController.StartDialogue(npc);
                            GameManager.Instance.Pause();
                        }
                        else if (npc.npcState == NPCManager.NPCState.injured)
                        {
                            //If injured
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            //Right click
            if (Input.GetMouseButton(1))
            {
                if (hit.collider != null && mouseDistance <= _clickDistanceLimit)
                {

                }
            }
        }

        //In charge of pausing and the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _enablePauseMenu = !_enablePauseMenu;

            _pauseMenu.SetActive(_enablePauseMenu);

            if (_enablePauseMenu)
            {
                if (!GameManager.Instance.CheckPaused())
                {
                    _pauseMenuPaused = true;
                    GameManager.Instance.Pause();
                }

                if (!GameManager.Instance.CheckStoppedClock())
                {
                    _pauseMenuStoppedClock = true;
                    GameManager.Instance.StopClock();
                }
            }
            else
            {
                if (_pauseMenuPaused == true)
                {
                    GameManager.Instance.UnPause();
                }

                if (_pauseMenuStoppedClock == true)
                {
                    GameManager.Instance.StartClock();
                }

                _pauseMenuPaused = false;
                _pauseMenuStoppedClock = false;
            }
        }
    }
}
