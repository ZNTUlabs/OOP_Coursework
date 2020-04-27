using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 2f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        rb.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { rb.velocity += Vector2.left * Speed; }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) rb.velocity += Vector2.right * Speed;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) rb.velocity += Vector2.up * Speed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) rb.velocity += Vector2.down * Speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "finish")
        {
            SceneManager.LoadScene("End");
        }
    }
}
