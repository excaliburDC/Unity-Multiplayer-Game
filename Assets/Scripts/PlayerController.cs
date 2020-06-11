using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourPun
{
    public enum MoveDirection
    {
        STOP = 0,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public MoveDirection moveDir;

    public float moveSpeed = 4f;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            InputButtons();
            MovePlayer();
        }
    }


    private void MovePlayer()
    {
        
        switch (moveDir)
        {
            case MoveDirection.STOP:
                rb2D.velocity = Vector2.zero;
                break;

            case MoveDirection.UP:
                rb2D.velocity = new Vector2(0, moveSpeed);
                break;

            case MoveDirection.DOWN:
                rb2D.velocity = new Vector2(0, -moveSpeed);
                break;

            case MoveDirection.LEFT:
                rb2D.velocity = new Vector2(-moveSpeed,0);
                break;

            case MoveDirection.RIGHT:
                rb2D.velocity = new Vector2(moveSpeed,0);
                break;

            
        }
    }

    private void InputButtons()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDir = MoveDirection.UP;
        }

        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = MoveDirection.DOWN;
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir = MoveDirection.LEFT;
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = MoveDirection.RIGHT;
        }

        else
        {
            moveDir = MoveDirection.STOP;
        }

    }

}
