using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Configuración de Aparición")]
    public GameObject collectiblePrefab;
    public float spawnInterval = 1.5f;
    public float minX = -8f;
    public float maxX = 8f;

    [SerializeField] private ControladorGameOver controladorGameOver;
    [SerializeField] private GameObject[] prefabsMalos;

    [Header("Dificultad progresiva")]
    [SerializeField, Min(1f)] private float segundosPorNivel = 12f;
    [SerializeField, Min(0.2f)] private float intervaloMinimo = 0.45f;
    [SerializeField, Min(0.1f)] private float velocidadInicial = 2f;
    [SerializeField, Min(0.1f)] private float velocidadMaxima = 6f;
    [SerializeField, Min(0f)] private float inicioPeligros = 8f;
    [SerializeField, Range(0f, 0.8f)] private float probabilidadInicial = 0.15f;
    [SerializeField, Range(0f, 0.8f)] private float probabilidadMaxima = 0.4f;

    private float tiempoDePartida;
    public float TiempoDePartida => tiempoDePartida;
    public float IntervaloActual => Mathf.Max(intervaloMinimo, spawnInterval - tiempoDePartida / segundosPorNivel * 0.18f);
    public float VelocidadActual => Mathf.Min(velocidadMaxima, velocidadInicial + tiempoDePartida / segundosPorNivel * 0.65f);
    public float ProbabilidadPeligroActual => tiempoDePartida < inicioPeligros ? 0f : Mathf.Min(probabilidadMaxima, probabilidadInicial + (tiempoDePartida - inicioPeligros) / segundosPorNivel * 0.05f);

    private void Start()
    {
        tiempoDePartida = 0f;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        tiempoDePartida += Time.deltaTime;
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            SpawnCollectible();
            yield return new WaitForSeconds(IntervaloActual);
        }
    }

    private void SpawnCollectible()
    {
        if (collectiblePrefab == null)
        {
            Debug.LogError("Falta el prefab del coleccionable.", this);
            return;
        }

        bool esMalo = prefabsMalos != null && prefabsMalos.Length > 0 && Random.value < ProbabilidadPeligroActual;
        GameObject prefab = esMalo ? prefabsMalos[Random.Range(0, prefabsMalos.Length)] : collectiblePrefab;
        if (prefab == null) return;
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
        GameObject objeto = Instantiate(prefab, spawnPosition, Quaternion.identity);
        if (objeto.TryGetComponent<FishMovement>(out var movimiento)) movimiento.fallSpeed = VelocidadActual;
        if (objeto.TryGetComponent<InteraccionGameOver>(out var interaccion)) interaccion.Inicializar(controladorGameOver);
        if (objeto.TryGetComponent<ObjetoMalo>(out var peligro)) peligro.Inicializar(controladorGameOver);
    }
}
