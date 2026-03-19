using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dame5 : MonoBehaviour
{
    [SerializeField] Collider2D puertaCollider;
    [SerializeField] Collider2D sensorCollider;
    [SerializeField] Animator animator;
    [SerializeField] float esperaFallback = 1f;

    bool puertaAbierta;
    bool cargandoEscena;

    void Start()
    {
        puertaCollider = GetComponent<Collider2D>();

        puertaAbierta = false;
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
        if (Input.touchCount != 5)
        {
            return;
        }

        
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                AbrirPuerta();
                return;
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
    }

}
