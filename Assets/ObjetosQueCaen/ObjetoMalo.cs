using UnityEngine;

public class ObjetoMalo : MonoBehaviour
{
    private ControladorGameOver controlador;
    private bool resuelto;

    public void Inicializar(ControladorGameOver controladorGameOver)
    {
        controlador = controladorGameOver;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (resuelto) return;
        if (other.CompareTag("Player"))
        {
            resuelto = true;
            if (controlador != null) controlador.ActivarGameOver();
            else Debug.LogError("Falta el controlador de Game Over del peligro.", this);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Suelo"))
        {
            resuelto = true;
            Destroy(gameObject);
        }
    }
}
