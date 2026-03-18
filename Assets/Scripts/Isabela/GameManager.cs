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
        StartCoroutine(SceneLoad(nivelSiguiente));
    }

    public static void CargarNivel2(int nivelIndex)
    {
        if (Instance != null)
            Instance.StartCoroutine(Instance.SceneLoad(nivelIndex));
    }

    private IEnumerator SceneLoad(int sceneIndex)
    {
        animator.SetBool("Start" , true);
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(sceneIndex);
        
        animator.SetBool("Start", false);
    }


    public void Salir() => Application.Quit();

}