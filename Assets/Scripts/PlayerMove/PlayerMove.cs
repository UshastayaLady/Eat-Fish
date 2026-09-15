using TMPro;
using Unity.Mathematics;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    private PlayerAction _playerAction;
    private Vector2 _inputDirection;
    private Vector3 _newDirection;
    private float _angle;

    [SerializeField] private float _rotationTime;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _sensetive;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _playerAction = new PlayerAction();
        _playerAction.Enable();
    }

    void FixedUpdate()
    {
        _inputDirection = _playerAction.Mover.Move.ReadValue<Vector2>();
        Move();
        Rotate();
    }

    private void OnDisable()
    {
        _playerAction.Disable();
        
    }

    private void Move()
    {
        Vector3 direction = new Vector3(
        _inputDirection.x,
        0,
        _inputDirection.y
    ).normalized;

        transform.Translate(
            direction * _speedMove * Time.fixedDeltaTime,
            Space.World
        );

    }


    // Для плавного поворота
    private void Rotate()
    {
        
        _newDirection = new Vector3(_inputDirection.x, 0f, _inputDirection.y).normalized;

        if (_newDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_newDirection.x, _newDirection.z) * Mathf.Rad2Deg;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _sensetive, _rotationTime);
            transform.rotation = Quaternion.Euler(0, _angle, 0);
        }
       
    }
}
