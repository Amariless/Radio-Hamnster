using System.Collections;
using UnityEngine;

public class BotonVibrador : MonoBehaviour
{
    [Header("Patrón de vibración")]
    [Tooltip("Duración de cada pulso de vibración en segundos")]
    public float duracionPulso = 0.2f;

    [Tooltip("Pausa entre pulsos en segundos")]
    public float intervaloEntrePulsos = 0.3f;

    private Coroutine _coroutineVibración;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (_coroutineVibración != null)
            StopCoroutine(_coroutineVibración);

        _coroutineVibración = StartCoroutine(VibrarIntermitente());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (_coroutineVibración != null)
        {
            StopCoroutine(_coroutineVibración);
            _coroutineVibración = null;
        }
    }

    IEnumerator VibrarIntermitente()
    {
        while (true)
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
            yield return new WaitForSeconds(duracionPulso);
            yield return new WaitForSeconds(intervaloEntrePulsos);
        }
    }

    void OnDisable()
    {
        if (_coroutineVibración != null)
        {
            StopCoroutine(_coroutineVibración);
            _coroutineVibración = null;
        }
    }
}