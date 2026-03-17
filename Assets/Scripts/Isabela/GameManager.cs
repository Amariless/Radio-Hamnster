using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    private static GameManager Instance { get; set; }

    private Animator animator;

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
        
       try {
            uiManager = FindObjectOfType<UiManager>();
        } catch (System.Exception ex) {
            Debug.LogError("No se encontró el UiManager en la escena: " + ex.Message);
        }

        animator = GetComponentInChildren<Animator>();
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

    public void Salir()
    {
        Application.Quit();
    }

    public void CargarNivel()
    {
        int nivelSiguiente = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(nivelSiguiente));
    }

    private IEnumerator SceneLoad(int sceneIndex)
    {
        animator.SetBool("Start" , true);
        yield return new WaitForSeconds(1f); // Opcional: agregar un retraso antes de cargar la escena
        SceneManager.LoadScene(sceneIndex);
        
        animator.SetBool("Start", false);
    }

}
