using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelNiveles;
    [SerializeField] private GameObject panelOpciones;
    private GameObject panelActual;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        panelMenu.SetActive(true);
        panelNiveles.SetActive(false);
        panelOpciones.SetActive(false);
        panelActual = panelMenu;
    }
    public void Iniciar()
    {
        panelMenu.SetActive(true);
        panelNiveles.SetActive(false);
        panelOpciones.SetActive(false);

        panelActual = panelMenu;

        int nivelGuardado = PlayerPrefs.GetInt("NivelActual", 1);
        gameManager.CargarNivel2(nivelGuardado);
    }

    public void Niveles()
    {
        panelActual.SetActive(false); 
        panelNiveles.SetActive(true); 
        panelActual = panelNiveles;  
    }

    public void Opciones()
    {
        panelActual.SetActive(false); 
        panelOpciones.SetActive(true);
        panelActual = panelOpciones; 
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
    
    public void Salir() => FindAnyObjectByType<GameManager>().Salir();  

    private void MostrarPanel(GameObject panel)
    {
        if (panelActual != null) panelActual.SetActive(false);
        panel.SetActive(true);
        panelActual = panel;
    }
}