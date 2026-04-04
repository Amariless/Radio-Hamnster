using UnityEngine;

public class sostener_puerta : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Collider2D zonaToqueCollider;

    void Update()
    {
        bool presionando = EstoyPresionandoPuerta();
        animator.SetBool("abierta", presionando);
    }

    bool EstoyPresionandoPuerta()
    {
        if (zonaToqueCollider == null || Camera.main == null)
            return false;

        // Mouse para pruebas en editor
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return FisicaToca(mousePos);
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                continue;

            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
            if (FisicaToca(pos)) return true;
        }

        return false;
    }

    bool FisicaToca(Vector2 punto)
    {
        // Detecta cualquier collider (incluidos triggers) en ese punto
        Collider2D resultado = Physics2D.OverlapPoint(punto, ~0, -100, 100);
        return resultado == zonaToqueCollider;
    }
}