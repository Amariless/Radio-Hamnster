using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour
{
    [SerializeField] Collider2D puertaCollider;
    bool puertaAbierta;
    [SerializeField] Animator animator;
    void Start()
    {
        puertaCollider = GetComponent<Collider2D>();
        puertaAbierta = false;
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
                Debug.Log("Tocando la pantalla en: " + touchPosition);
                if (puertaCollider != null && puertaCollider.OverlapPoint(touchPosition))
                {
                    Debug.Log("Tocando la puerta");
                    AbrirPuerta();
                }
            }
        }
    }

    void AbrirPuerta()
    {
        if (!puertaAbierta)
        {
            puertaAbierta = true;
            Debug.Log("Abriendo la puerta");
            puertaCollider.enabled = false;
            if (animator != null)
            {
                animator.SetTrigger("abre");
            }
            
        }
    }
}
