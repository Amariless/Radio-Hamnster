using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta101 : MonoBehaviour
{
    [SerializeField] Collider2D puertaCollider;
    bool puertaAbierta;
    [SerializeField] Animator animator;
    [SerializeField] int toquesParaAbrir = 30;
    int toquesActuales;

    void Start()
    {
        puertaCollider = GetComponent<Collider2D>();
        puertaAbierta = false;
        toquesActuales = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (puertaAbierta)
        {
            return;
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                if (puertaCollider != null && puertaCollider.OverlapPoint(touchPosition))
                {
                    toquesActuales++;
                    

                    if (toquesActuales >= toquesParaAbrir)
                    {
                        AbrirPuerta();
                    }
                }
            }
        }
    }

    void AbrirPuerta()
    {
        if (!puertaAbierta)
        {
            puertaAbierta = true;
            puertaCollider.enabled = false;
            if (animator != null)
            {
                animator.SetTrigger("abre");
            }
            
        }
    }
}
