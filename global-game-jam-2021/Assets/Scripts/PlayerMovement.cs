﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;  // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;   // A position marking where to check if the player is grounded.

    public float speed;
    public float jumpForce;

    bool canJump = false;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            movement.x -= Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            movement.x += Time.deltaTime * speed;
        }

        transform.Translate(movement);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump) {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        bool prevJump = canJump;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.2f, m_WhatIsGround);

        canJump = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				canJump = true;
			}
		}
    }
}
