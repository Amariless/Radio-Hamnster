using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; set; }
    private Animator animator;
    public Animator animatorMarie;

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
        animatorMarie = GameObject.Find("Marie").GetComponent<Animator>();
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
            animatorMarie.SetBool("Entra", false); 
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
        StartCoroutine(AnimationMarie());
    

    if (animator != null)
        animator.SetBool("Start", false);
        
}

    IEnumerator AnimationMarie()
{
    letras scriptLetras = GameObject.Find("Marie").GetComponentInChildren<letras>(); // busca en los hijos de Marie
    Debug.Log("elemento encontrado: " + (scriptLetras != null ? "sí" : "no"));


    if (animatorMarie != null)
    {
        animatorMarie.SetBool("Entra", true);

        if (scriptLetras != null)
            scriptLetras.Activar();

        yield return new WaitForSeconds(3f);
        animatorMarie.SetBool("Entra", false);
    }
}


    public void Salir() => Application.Quit();

}