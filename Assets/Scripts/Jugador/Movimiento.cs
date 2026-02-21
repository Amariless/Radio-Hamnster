using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;

    [Header("Botones UI")]
    public Button botonIzquierdo;
    public Button botonDerecho;
    public Button botonSalto;

    private Rigidbody2D rb;
    private bool enPiso;
    private float direccionMovimiento = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        AddHoldListener(botonIzquierdo,  () => direccionMovimiento = -1f, () => direccionMovimiento = 0f);
        AddHoldListener(botonDerecho, () => direccionMovimiento =  1f, () => direccionMovimiento = 0f);

        botonSalto.onClick.AddListener(Jump);
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(direccionMovimiento * velocidad, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (enPiso)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            enPiso = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            enPiso = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            enPiso = false;
    }

    void AddHoldListener(Button btn, UnityEngine.Events.UnityAction onPress, UnityEngine.Events.UnityAction onRelease)
    {
        var trigger = btn.gameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>()
                   ?? btn.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

        var pressEntry = new UnityEngine.EventSystems.EventTrigger.Entry
            { eventID = UnityEngine.EventSystems.EventTriggerType.PointerDown };
        pressEntry.callback.AddListener(_ => onPress());
        trigger.triggers.Add(pressEntry);

        var releaseEntry = new UnityEngine.EventSystems.EventTrigger.Entry
            { eventID = UnityEngine.EventSystems.EventTriggerType.PointerUp };
        releaseEntry.callback.AddListener(_ => onRelease());
        trigger.triggers.Add(releaseEntry);
    }
}