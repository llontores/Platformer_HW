using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction<float> HorizontalInputChanged;
    public event UnityAction JumpButtonPressed;
    public event UnityAction AttackButtonPressed;
    public event UnityAction<bool> JumpingStateChanged;

    private float _horizontalInput;
    private bool _isGrounded;

    private void Start()
    {
        _isGrounded = true;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(_horizontalInput) > 0.01)
        {
            float sign = Mathf.Sign(_horizontalInput);
            HorizontalInputChanged?.Invoke(_horizontalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            JumpButtonPressed?.Invoke();
            JumpingStateChanged(_isGrounded);
        }

        if (Input.GetMouseButtonDown(0))
            AttackButtonPressed?.Invoke();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            _isGrounded = true;
            JumpingStateChanged(_isGrounded);
        }
    }
}
