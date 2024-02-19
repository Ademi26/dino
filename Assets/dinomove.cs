using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinomove : MonoBehaviour
{
    // Переменные для управления анимациями
    private Animator animator;
    private bool isRunning = false;
    private bool isJumping = false;

    // Переменные для управления движением и прыжком
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    // Вызывается при запуске игры
    void Start()
    {
        // Получаем компоненты Animator и Rigidbody2D
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Вызывается на каждом кадре
    void Update()
    {
        // Управление бегом
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Управление анимацией бега
        if (moveInput != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Управление прыжком
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        // Управление анимацией прыжка
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        // Управление анимацией бега
        animator.SetBool("IsRunning", isRunning);
    }

    // Вызывается, когда персонаж касается земли
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}