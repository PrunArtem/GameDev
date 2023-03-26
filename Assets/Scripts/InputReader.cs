using UnityEngine;
using Player;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;
    
    private float _direction;

    private bool _jump = false;

    void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Jump"))
        {
            _jump = true;
        }
    }

    private void FixedUpdate()
    {
        _playerEntity.MoveHorizontally(_direction);
        if (_jump) 
        {
            _playerEntity.Jump();
            _jump = false;
        }
    }
}
