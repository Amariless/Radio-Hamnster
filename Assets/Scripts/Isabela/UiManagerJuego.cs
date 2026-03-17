using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagerJuego : MonoBehaviour
{
    private static UiManagerJuego Instance { get; set; }
    [SerializeField] private GameObject panelPausa;
    

    public void Pausa()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
