using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    Camera camara;
    bool arrastrando = false;

    void Start()
    {
        camara = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch toque = Input.GetTouch(0);
        Vector3 posicionMundo = camara.ScreenToWorldPoint(toque.position);
        posicionMundo.z = transform.position.z;

        if (toque.phase == TouchPhase.Began)
        {
            Collider2D col = Physics2D.OverlapPoint(posicionMundo, LayerMask.GetMask("Plataforma"));
            if (col != null && col.gameObject == gameObject)
                arrastrando = true;
        }
        else if (toque.phase == TouchPhase.Ended || toque.phase == TouchPhase.Canceled)
        {
            arrastrando = false;
        }

        if (arrastrando)
            transform.position = posicionMundo;
    }
}