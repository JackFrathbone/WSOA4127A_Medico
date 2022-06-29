using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("Settings")]
    //How far from the player the target can be activated
    [SerializeField] float _clickDistanceLimit;
    public bool disableInput;

    [Header("References")]
    private PlayerController _playerController;
    private DialogueController _dialogueController;

    private void Awake()
    {
        GameManager.instance.PlayerInputController = this;
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _dialogueController = GetComponent<DialogueController>();
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
                    if (hit.collider.tag == "NPC")
                    {
                        NPCManager npc = hit.collider.GetComponent<NPCManager>();
                        if(npc.npcState == NPCManager.NPCState.alive)
                        {
                            _dialogueController.StartDialogue(npc);
                            GameManager.instance.Pause();
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
    }
}
