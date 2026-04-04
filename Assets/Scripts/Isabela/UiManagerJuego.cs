using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagerJuego : MonoBehaviour
{
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }
    public void Pausa()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f; 

    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void VolverMenu()
    {
        Time.timeScale = 1f;
        if (gameManager != null && gameManager.animatorMarie != null)
            gameManager.animatorMarie.SetBool("Entra", false);

        SceneManager.LoadScene("Nivel0");
    }
    
}
