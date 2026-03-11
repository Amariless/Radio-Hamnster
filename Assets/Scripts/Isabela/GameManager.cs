using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelNiveles;
    [SerializeField] private GameObject panelOpciones;
    [SerializeField] private GameObject panelPausa;
    private GameObject panelActual;

    private static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        panelMenu.SetActive(true);
        panelNiveles.SetActive(false);
        panelOpciones.SetActive(false);
        panelActual = panelMenu;
        panelPausa.SetActive(false);
    }
    public void Iniciar()
    {
        panelMenu.SetActive(true);
        panelNiveles.SetActive(false);
        panelOpciones.SetActive(false);

        panelActual = panelMenu;
    }

    public void Niveles()
    {
        panelActual.SetActive(false); 
        panelNiveles.SetActive(true); 
        panelActual = panelNiveles;  
    }

    public void Opciones()
    {
        // Cambiar al panel de opciones
        panelActual.SetActive(false); // Desactivar el panel actual
        panelOpciones.SetActive(true); // Activar el panel de opciones
        panelActual = panelOpciones;  // Actualizar el panel actual
    }

    public void Volver()
    {
        
        if (panelActual != null)
        {
            panelActual.SetActive(false); 
        }

        panelMenu.SetActive(true); 
        panelActual = panelMenu;  
    }

    public void Pausa()
    {
        Time.timeScale = 0f; 
        panelPausa.SetActive(true);
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
