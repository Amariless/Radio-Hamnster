using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorNivel : MonoBehaviour
{
    public GameObject nivelButtonPrefab;
    public Transform buttonContainer;
    public int totalNiveles = 16;

    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        for (int i = 1; i <= totalNiveles; i++)
        {
            GameObject buttonObj = Instantiate(nivelButtonPrefab, buttonContainer);

            // Set the button text  
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Nivel " + i;

            int nivelIndex = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Nivel" + nivelIndex);
                Debug.Log("Cargando Nivel " + nivelIndex);
            });
        }
    }
}
