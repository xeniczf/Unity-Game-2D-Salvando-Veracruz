using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float frequency = 4f; // Velocidad del movimiento de lado a lado
    public float magnitude = 0.5f; // Qu� tan ancho es el movimiento de lado a lado

    private float startX;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startX = body.position.x;
    }

    void FixedUpdate()
    {
        float x = startX + Mathf.Sin(Time.fixedTime * frequency) * magnitude;
        body.linearVelocity = new Vector2((x - body.position.x) / Time.fixedDeltaTime, -fallSpeed);
    }

    // La recogida y los impactos los gestionan Collectible y ObjetoMalo.

}