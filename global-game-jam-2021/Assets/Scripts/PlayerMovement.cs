using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;  // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;   // A position marking where to check if the player is grounded.

    public float speed;
    public float jumpForce;

    Animator animator;

    public bool canSwimInLava = false;
    public bool canSwimInWater = false;
    bool canJump = false;
    bool isFacingRight = true;

    public bool canMine = true;
    public GameObject tilemapGameObject;

    Tilemap tilemap;

    Rigidbody2D rb;
    InventoryManager im;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        im = GetComponent<InventoryManager>();
        animator = GetComponent<Animator>();

        if (tilemapGameObject != null)
        {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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

        if (movement.x > 0 && !isFacingRight) {
            Flip();
        } else if (movement.x < 0 && isFacingRight) {
            Flip();
        }

        animator.SetBool("IsMoving",
            (movement.x > 0 || movement.x < 0));

        transform.Translate(movement);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump) {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == 9 && !canSwimInLava) || (other.gameObject.layer == 4 && !canSwimInWater))
            SceneManager.LoadScene("SampleScene");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null && tilemapGameObject == collision.gameObject && canMine)
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
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

        if (!im.EquippedFeatureBoxesContainsName("Jump"))
            canJump = false;

        canMine = im.EquippedFeatureBoxesContainsName("Mine");
        canSwimInLava = im.EquippedFeatureBoxesContainsName("Lava");
        canSwimInWater = im.EquippedFeatureBoxesContainsName("Water");
    }
}
