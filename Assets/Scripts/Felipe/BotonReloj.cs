using UnityEngine;

public class BotonReloj : MonoBehaviour
{
    public enum TipoBoton { MasHora, MenosHora, MasMinuto, MenosMinuto }

    [SerializeField] private TipoBoton tipo;
    [SerializeField] private RelojNivel reloj;

    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.touchCount <= 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        Vector3 posicionToque = Camera.main.ScreenToWorldPoint(touch.position);

        if (col != null && col.OverlapPoint(posicionToque))
        {
            switch (tipo)
            {
                case TipoBoton.MasHora:     reloj.ModificarHora(1);    break;
                case TipoBoton.MenosHora:   reloj.ModificarHora(-1);   break;
                case TipoBoton.MasMinuto:   reloj.ModificarMinuto(1);  break;
                case TipoBoton.MenosMinuto: reloj.ModificarMinuto(-1); break;
            }
        }
    }
}