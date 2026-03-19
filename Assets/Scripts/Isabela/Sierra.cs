using UnityEngine;

public class Sierra : MonoBehaviour
{
    [Header("Rotación")]
    public float velocidadRotacion = 360f;
    private float velocidadActual;

    [Header("Frenado")]
    public float desaceleración;
    
    [Header("Collider")]
    public float escalaColiderActivo = 1f;
    public float escalaColiderDetenido = 0.3f;
    
    private Collider2D sierraCollider;
    private Camera camara;
    private bool tocandoSierra;
    private bool sierraDetenida;

    void Start()
    {
        velocidadActual = velocidadRotacion;
        sierraCollider = GetComponent<Collider2D>();
        camara = Camera.main;
        sierraDetenida = false;
    }

    void Update()
    {
        // Girar sobre su propio eje (Z)
        transform.Rotate(0f, 0f, velocidadActual * Time.deltaTime);

        // Detectar toque
        DetectarToque();

        if (tocandoSierra && velocidadActual > 0f)
        {
            velocidadActual -= desaceleración * Time.deltaTime;
        
            if (velocidadActual <= 0f)
            {
                velocidadActual = 0f;
            }
        }

        // Ajustar collider según estado
        AjustarCollider();
    }

    void DetectarToque()
    {
        tocandoSierra = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = camara.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (sierraCollider != null && sierraCollider.OverlapPoint(touchPosition))
            {
                tocandoSierra = true;
            }
        }
    }

    void AjustarCollider()
    {
        bool detenidoAhora = velocidadActual <= 0f;

        if (detenidoAhora != sierraDetenida)
        {
            sierraDetenida = detenidoAhora;

            if (sierraCollider is BoxCollider2D boxCollider)
            {
                boxCollider.size = boxCollider.size / (detenidoAhora ? escalaColiderActivo / escalaColiderDetenido : escalaColiderDetenido / escalaColiderActivo);
            }
            else if (sierraCollider is CircleCollider2D circleCollider)
            {
                circleCollider.radius *= (detenidoAhora ? escalaColiderDetenido : escalaColiderActivo);
            }
        }
    }
}

