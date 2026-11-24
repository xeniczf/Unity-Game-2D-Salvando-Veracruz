using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI scoreText;

    [Header("Fin del nivel")]
    [SerializeField, Min(1)] private int puntosObjetivo = 2500;
    [SerializeField] private ControladorGameOver controladorFinNivel;

    private int score = 0;
    public int Score => score;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetInstance()
    {
        Instance = null;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Hay más de un ScoreManager en la escena.", this);
            enabled = false;
            return;
        }

        Instance = this;
        score = 0;
        UpdateScoreText();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void AddScore(int amount)
    {
        if (controladorFinNivel != null && controladorFinNivel.NivelTerminado) return;
        score += amount;
        UpdateScoreText();
        if (score >= puntosObjetivo && controladorFinNivel != null)
            controladorFinNivel.CompletarNivel();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.SetText("{0}", score);
        else
            Debug.LogError("Asigna el contador de puntos al ScoreManager.", this);
    }
}
