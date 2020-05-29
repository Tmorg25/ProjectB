using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class NG_movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 lastMovement;

    public Animator animator;

    private Camera cam;

    public Transform firePoint;
    public GameObject bullet;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //input

        handleMovement();
        mouseDirection();

        

    }

    void FixedUpdate()
    {
        //movement 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void handleMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x  != 0 || movement.y != 0)
        {
            lastMovement.x = movement.x;
            lastMovement.y = movement.y;
        }
        else
        {
            animator.SetFloat("lastHorizontal", lastMovement.x);
            animator.SetFloat("lastVertical", lastMovement.y);
        }

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        
    }

    void mouseDirection()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 screenpoint = cam.WorldToScreenPoint(firePoint.position);

        Vector2 offset = new Vector2(mouse.x - screenpoint.x, mouse.y - screenpoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(1))
        {

            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }



    }
}
