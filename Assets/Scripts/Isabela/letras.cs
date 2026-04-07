using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class letras : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI texto;
    [SerializeField] float velocidad = 0.5f;

    // el índice del array = el buildIndex de la escena
    [TextArea] public string[] mensajes = {
        "",                          // escena 0 - menú
        "Mensaje del nivel 1",       // escena 1
        "Mensaje del nivel 2",       // escena 2
        "Mensaje del nivel 3",       // escena 3
    };

    public void Activar(int sceneIndex)  // <-- recibe el índice
{
    if (sceneIndex < mensajes.Length && mensajes[sceneIndex] != "")
        StartCoroutine(EscribirTexto(mensajes[sceneIndex]));
}

    IEnumerator EscribirTexto(string mensaje)
    {
        texto.text = "";
        foreach (char letra in mensaje)
        {
            texto.text += letra;
            yield return new WaitForSeconds(velocidad);
        }
    }
}