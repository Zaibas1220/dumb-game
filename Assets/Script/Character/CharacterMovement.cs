using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float speed;

    private DumbGameInputs.PlayerActions _inputs;

    private bool _onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        _inputs = new DumbGameInputs().Player;
        _inputs.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Jump();
    }

    private void Jump()
    {
        int jump = (int) _inputs.Jump.ReadValue<float>();
        if (_onGround && jump == 1)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpStrength);
            _onGround = false;
        }
    }

    private void MoveCharacter()
    {
        float movement = _inputs.Movement.ReadValue<float>();
        _rigidbody2D.velocity = new Vector2(movement * speed, _rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Ground
        if (col.gameObject.layer == 3)
            _onGround = true;
    }
}
