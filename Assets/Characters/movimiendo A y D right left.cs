using UnityEngine;
using UnityEngine.InputSystem; // Importante para usar el Nuevo Input System

public class PlayerController : MonoBehaviour
{
    [Header("Configuración")]
    public float Velocidad = 5f;

    [Header("Referencias")]
    public Animator animator; // Arrastra tu Animator aquí en el Inspector

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Seguridad por si olvidas asignar el Animator en el Inspector
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        float movimientoH = 0f;

        // Leemos el teclado mediante el Nuevo Input System sin generar conflictos
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                movimientoH = 1f;
            }
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                movimientoH = -1f;
            }
        }

        // Mandamos el valor absoluto al parámetro "movement" del Animator
        if (animator != null)
        {
            animator.SetFloat("movement", Mathf.Abs(movimientoH));
        }

        // Volteamos el sprite horizontalmente según la dirección
        if (movimientoH > 0.01f)
        {
            spriteRenderer.flipX = false; // Mirando a la derecha
        }
        else if (movimientoH < -0.01f)
        {
            spriteRenderer.flipX = true;  // Mirando a la izquierda
        }
    }

    void FixedUpdate()
    {
        float movimientoH = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) movimientoH = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) movimientoH = -1f;
        }

        // Aplicamos el movimiento físico en el Rigidbody2D
        rb.linearVelocity = new Vector2(movimientoH * Velocidad, rb.linearVelocity.y);
    }
}