using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; set; }
    private Animator animator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        animator = GetComponentInChildren<Animator>();
    }

    public void CargarNivel()
    {
        int nivelSiguiente = SceneManager.GetActiveScene().buildIndex + 1;
    // Si el siguiente nivel no existe, es el último
        if (nivelSiguiente >= SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.DeleteKey("NivelActual"); // borra el progreso
            PlayerPrefs.SetInt("NivelActual", 1);
            PlayerPrefs.Save();
        // Aquí puedes cargar el menú principal o la escena 0
            StartCoroutine(SceneLoad(0)); // ← cambia 0 por el índice de tu menú
        }
        else
        {
            StartCoroutine(SceneLoad(nivelSiguiente));
        }
    }

    public void CargarNivel2(int nivelIndex)
    {
        StartCoroutine(SceneLoad(nivelIndex));
    }

    private IEnumerator SceneLoad(int sceneIndex)
{
    if (animator != null)
        animator.SetBool("Start", true);

    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene("Nivel" + sceneIndex);

    if (animator != null)
        animator.SetBool("Start", false);
}


    public void Salir() => Application.Quit();

}