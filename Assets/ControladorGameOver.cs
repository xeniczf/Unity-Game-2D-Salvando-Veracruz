using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGameOver : MonoBehaviour
{
    public GameObject panelGameOver;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private UnityEngine.UI.Button botonJugar;
    [SerializeField] private UnityEngine.UI.Button botonReintentar;
    [SerializeField] private UnityEngine.UI.Button botonMenu;
    [SerializeField] private GameObject panelVictoria;

    private bool nivelTerminado;
    public bool NivelTerminado => nivelTerminado;

    private static bool iniciarDirectamente;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void RestablecerInicio()
    {
        iniciarDirectamente = false;
    }

    private void Awake()
    {
        nivelTerminado = false;
        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        bool mostrarMenu = panelMenu != null && !iniciarDirectamente;
        iniciarDirectamente = false;
        if (panelMenu != null) panelMenu.SetActive(mostrarMenu);
        Time.timeScale = mostrarMenu ? 0f : 1f;
    }

    private void OnEnable()
    {
        if (botonJugar != null) botonJugar.onClick.AddListener(Jugar);
        if (botonReintentar != null) botonReintentar.onClick.AddListener(ReiniciarJuego);
        if (botonMenu != null) botonMenu.onClick.AddListener(VolverAlMenu);
    }

    private void OnDisable()
    {
        if (botonJugar != null) botonJugar.onClick.RemoveListener(Jugar);
        if (botonReintentar != null) botonReintentar.onClick.RemoveListener(ReiniciarJuego);
        if (botonMenu != null) botonMenu.onClick.RemoveListener(VolverAlMenu);
    }

    public void Jugar()
    {
        nivelTerminado = false;
        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelMenu != null) panelMenu.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CompletarNivel()
    {
        if (nivelTerminado) return;
        nivelTerminado = true;
        if (panelMenu != null) panelMenu.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ActivarGameOver()
    {
        if (nivelTerminado) return;
        if (panelGameOver != null)
        {
            nivelTerminado = true;
            if (panelMenu != null) panelMenu.SetActive(false);
            panelGameOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReiniciarJuego()
    {
        iniciarDirectamente = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        iniciarDirectamente = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}