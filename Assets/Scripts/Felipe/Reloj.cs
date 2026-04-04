using System;
using UnityEngine;
using TMPro;

public class RelojNivel : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private TextMeshPro textoReloj;

    [Header("Puerta")]
    [SerializeField] private puerta101 puerta;

    [Header("Tolerancia")]
    [Tooltip("Minutos de margen aceptados. 0 = hora exacta.")]
    public int toleranciaMinutos = 0;

    private int horaActual = 0;
    private int minutoActual = 0;
    private bool puertaAbierta = false;

    void Start()
    {
        horaActual = 0;
        minutoActual = 0;
        ActualizarDisplay();
    }

    void Update()
    {
        if (puertaAbierta) return;
        VerificarHora();
    }

    void VerificarHora()
    {
        DateTime ahora = DateTime.Now;
        int diferenciaHoras = Mathf.Abs(horaActual - ahora.Hour);
        int diferenciaMinutos = Mathf.Abs(minutoActual - ahora.Minute);

        if (diferenciaHoras > 12) diferenciaHoras = 24 - diferenciaHoras;

        bool horaCorrecta = diferenciaHoras == 0 && diferenciaMinutos <= toleranciaMinutos;

        if (horaCorrecta)
        {
            puertaAbierta = true;
            puerta.AbrirPuerta();
        }
    }

    public void ModificarHora(int cantidad)
    {
        horaActual = (horaActual + cantidad + 24) % 24;
        ActualizarDisplay();
    }

    public void ModificarMinuto(int cantidad)
    {
        minutoActual = (minutoActual + cantidad + 60) % 60;
        ActualizarDisplay();
    }

    void ActualizarDisplay()
    {
        textoReloj.text = horaActual.ToString("D2") + ":" + minutoActual.ToString("D2");
    }
}