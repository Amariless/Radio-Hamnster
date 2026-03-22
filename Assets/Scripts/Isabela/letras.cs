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

    public void Activar()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex + 1;
        if (escenaActual < mensajes.Length && mensajes[escenaActual] != "")
            StartCoroutine(EscribirTexto(mensajes[escenaActual]));
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