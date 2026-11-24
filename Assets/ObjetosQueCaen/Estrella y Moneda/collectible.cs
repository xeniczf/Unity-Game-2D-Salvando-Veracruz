using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Puntuaci�n")]
    public int pointsToAdd = 1; // Puntos que otorga este objeto (puedes cambiarlo a 10 desde el Inspector)

    private bool collected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collect(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collect(other);
    }

    private void Collect(Collider2D other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true;
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(pointsToAdd);
            }

            Destroy(gameObject);
        }
    }
}