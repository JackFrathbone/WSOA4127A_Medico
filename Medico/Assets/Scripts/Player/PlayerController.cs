using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _playerSpeed = 1f;
    public bool disableMovement;

    [Header("References")]
    private Rigidbody2D _rb;

    private void Awake()
    {
        GameManager.Instance.playerController = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!disableMovement)
        {
            Vector2 currentPos = _rb.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * _playerSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            _rb.MovePosition(newPos);

            //For setting the player direction based on mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Vector3 charScale = transform.localScale;
            if (mousePos.x > newPos.x)
            {
                charScale.x = -1;
            }
            else if (mousePos.x < newPos.x)
            {
                charScale.x = 1;
            }
            transform.localScale = charScale;
        }
    }
}