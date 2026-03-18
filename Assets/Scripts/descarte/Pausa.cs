using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    private UiManagerJuego uiManager;

    private void Start()
    {
        uiManager = FindAnyObjectByType<UiManagerJuego>();
    }

    public void OnClickPausa()
    {
        uiManager.Pausa();
    }
}