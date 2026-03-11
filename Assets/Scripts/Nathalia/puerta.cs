using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puerta : MonoBehaviour
{
    [SerializeField] Collider2D puertaCollider;
    [SerializeField] Collider2D sensorCollider;
    [SerializeField] Animator animator;
    [SerializeField] float esperaFallback = 1f;

    bool puertaAbierta;
    bool animacionTerminada;
    bool cargandoEscena;

    void Start()
    {
        puertaCollider = GetComponent<Collider2D>();

        puertaAbierta = false;
        animacionTerminada = false;
        cargandoEscena = false;
    }

    void Update()
    {
        if (!puertaAbierta)
        {
            RevisarToquePuerta();
        }
    }

    void RevisarToquePuerta()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            if (puertaCollider != null && puertaCollider.OverlapPoint(touchPosition))
            {
                AbrirPuerta();
            }
        }
    }

    void AbrirPuerta()
    {
        if (!puertaAbierta && !cargandoEscena)
        {
            puertaAbierta = true;
            puertaCollider.enabled = false;

            if (animator != null)
            {
                animator.SetTrigger("abre");
            }

            StartCoroutine(EsperarAnimacionApertura());
        }
    }

    IEnumerator EsperarAnimacionApertura()
    {
        yield return null;

        float esperaAnimacion = esperaFallback;
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.length > 0f)
            {
                esperaAnimacion = stateInfo.length;
            }
        }

        yield return new WaitForSeconds(esperaAnimacion);
        animacionTerminada = true;
    }

}
