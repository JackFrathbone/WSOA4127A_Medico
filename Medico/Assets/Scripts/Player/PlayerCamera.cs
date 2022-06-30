using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothTime;

    private Vector2 _velocity;

    private Camera _cam;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, _smoothTime);
    }
}