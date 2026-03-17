using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelNiveles;
    [SerializeField] private GameObject panelOpciones;
    private GameObject panelActual;

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

    public void Niveles() => MostrarPanel(panelNiveles);
    public void Opciones() => MostrarPanel(panelOpciones);
    public void Volver() => MostrarPanel(panelMenu);
    public void Salir() => FindAnyObjectByType<GameManager>().Salir();  

    private void MostrarPanel(GameObject panel)
    {
        if (panelActual != null) panelActual.SetActive(false);
        panel.SetActive(true);
        panelActual = panel;
    }
}
