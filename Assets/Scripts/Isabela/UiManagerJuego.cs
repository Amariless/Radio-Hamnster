using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagerJuego : MonoBehaviour
{
    [SerializeField] private GameObject panelPausa;
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
        SceneManager.LoadScene("Menu");
    }
}
