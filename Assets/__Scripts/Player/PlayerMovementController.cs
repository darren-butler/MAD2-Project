﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform t;

    [SerializeField] private float vSpeed = 15.0f;
    [SerializeField] private float hSpeed = 10.0f;

    [SerializeField] private int dashFactor = 5;
    [SerializeField] private float startDashTime = 0.1f;
    private float dashTime;
    private bool dash;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        t = GetComponent<Transform>();
    }

    void Update()
    {
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && hMovement != 0)
        {
            dash = true;
            dashTime = startDashTime;
        }

        if (dash)
        {
            dashMovement(hMovement, vMovement);
        }
        else
        {
            rb.velocity = new Vector2(hMovement * hSpeed, vMovement * vSpeed);
        }

        //t.up = rb.velocity.normalized;

        sr.flipY = flipSprite(hMovement, vMovement);

        clampPosition();

    }

    private void clampPosition()
    {
        float yValue = Mathf.Clamp(rb.position.y, -4.75f, 4.75f);
        float xValue = Mathf.Clamp(rb.position.x, -8.9f, 8.9f);

        rb.position = new Vector2(xValue, yValue);
    }

    private void dashMovement(float hMovement, float vMovement)
    {
        dashTime -= Time.deltaTime;

        if (dashTime <= 0)
        {
            dash = false;
        }
        else
        {
            rb.velocity = new Vector2(hMovement * hSpeed * dashFactor, vMovement * vSpeed);
        }
    }

    private bool flipSprite(float hMovement, float vMovement)
    {
        if (hMovement < 0)
        {
            return true;
        }

        return false;
    }

}
