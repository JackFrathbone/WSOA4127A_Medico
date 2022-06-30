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
        }
    }
}