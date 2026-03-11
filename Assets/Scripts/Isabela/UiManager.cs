using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager Instance { get; set; }
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelNiveles;
    [SerializeField] private GameObject panelOpciones;
    private GameObject panelActual;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
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
}
