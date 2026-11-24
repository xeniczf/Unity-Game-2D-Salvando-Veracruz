using UnityEngine;

public class InteraccionGameOver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            ActivarFinDeJuego();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            ActivarFinDeJuego();
        }
    }

    private ControladorGameOver controlador;
    private bool terminado;

    public void Inicializar(ControladorGameOver controladorGameOver)
    {
        controlador = controladorGameOver;
    }

    private void ActivarFinDeJuego()
    {
        if (terminado) return;
        terminado = true;

        if (controlador != null)
        {
            controlador.ActivarGameOver();
        }
        else
        {
            Debug.LogError("El coleccionable no tiene un controlador de Game Over asignado.", this);
        }

        Destroy(gameObject);
    }
}