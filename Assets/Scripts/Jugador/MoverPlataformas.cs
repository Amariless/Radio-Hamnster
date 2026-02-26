using UnityEngine;

public class MoverPlataformas : MonoBehaviour
{
    Camera camara;
    Collider2D plataformaCollider;
    Rigidbody2D jugadorRb;
    bool jugadorEncima;
    [SerializeField] LayerMask plataformasLayer = ~0;
    [SerializeField] float margenColision = 0.01f;
    readonly RaycastHit2D[] hits = new RaycastHit2D[8];
    bool arrastrando;
    int dedoArrastreId = -1;

    void Start()
    {
        camara = Camera.main;
        plataformaCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = camara.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f; // Asegura que la posición esté en el plano 2D

            if (touch.phase == TouchPhase.Began)
            {
                if (plataformaCollider != null && plataformaCollider.OverlapPoint(touchPosition))
                {
                    arrastrando = true;
                    dedoArrastreId = touch.fingerId;
                    MoverA(touchPosition);
                }
            }
            else if (arrastrando && touch.fingerId == dedoArrastreId &&
                     (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                MoverA(touchPosition);
            }
            else if (arrastrando && touch.fingerId == dedoArrastreId &&
                     (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                arrastrando = false;
                dedoArrastreId = -1;
            }
        }
        else
        {
            arrastrando = false;
            dedoArrastreId = -1;
        }
    }

    void MoverA(Vector3 objetivo)
    {
        if (plataformaCollider == null)
        {
            Vector3 posicionAnterior = transform.position;
            transform.position = objetivo;
            MoverJugadorConPlataforma(transform.position - posicionAnterior);
            return;
        }

        Vector2 posicionActual = transform.position;
        Vector2 delta = (Vector2)objetivo - posicionActual;
        if (delta == Vector2.zero)
        {
            return;
        }

        ContactFilter2D filtro = new ContactFilter2D();
        filtro.SetLayerMask(plataformasLayer);
        filtro.useLayerMask = true;
        filtro.useTriggers = false;

        int cantidad = plataformaCollider.Cast(delta.normalized, filtro, hits, delta.magnitude);
        if (cantidad == 0)
        {
            Vector3 posicionAnterior = transform.position;
            transform.position = objetivo;
            MoverJugadorConPlataforma(transform.position - posicionAnterior);
            return;
        }

        float distanciaMinima = delta.magnitude;
        for (int i = 0; i < cantidad; i++)
        {
            if (hits[i].distance < distanciaMinima)
            {
                distanciaMinima = hits[i].distance;
            }
        }

        float distanciaMover = Mathf.Max(0f, distanciaMinima - margenColision);
        Vector2 nuevaPosicion = posicionActual + delta.normalized * distanciaMover;
        Vector3 posicionAnteriorFinal = transform.position;
        transform.position = nuevaPosicion;
        MoverJugadorConPlataforma(transform.position - posicionAnteriorFinal);
    }

    void MoverJugadorConPlataforma(Vector3 deltaPlataforma)
    {
        if (!jugadorEncima || deltaPlataforma == Vector3.zero)
        {
            return;
        }

        if (jugadorRb != null)
        {
            jugadorRb.MovePosition(jugadorRb.position + (Vector2)deltaPlataforma);
        }
        else
        {
            Transform jugadorTransform = transform;
            if (jugadorTransform != null)
            {
                jugadorTransform.position += deltaPlataforma;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorEncima = true;
            jugadorRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorEncima = false;
            jugadorRb = null;
        }
    }
}
