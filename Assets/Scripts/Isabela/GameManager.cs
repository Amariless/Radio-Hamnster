using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
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
        }
    }
    private void Start()
    {
       try {
            uiManager = FindObjectOfType<UiManager>();
        } catch (System.Exception ex) {
            Debug.LogError("No se encontró el UiManager en la escena: " + ex.Message);
        }
    }
    public void Iniciar()
    {
        uiManager.Iniciar();
    }

    public void Niveles()
    {
        uiManager.Niveles();
    }

    public void Opciones()
    {
        uiManager.Opciones();
    }

    public void Volver()
    {
        uiManager.Volver();
    }

}
