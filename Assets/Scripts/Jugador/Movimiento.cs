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
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool enPiso;
    private float direccionMovimiento = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        AddHoldListener(botonIzquierdo,  () => direccionMovimiento = -1f, () => direccionMovimiento = 0f);
        AddHoldListener(botonDerecho, () => direccionMovimiento =  1f, () => direccionMovimiento = 0f);

        botonSalto.onClick.AddListener(Jump);
    }

    void Update()
    {
        rb.velocity = new Vector2(direccionMovimiento * velocidad, rb.velocity.y);
        Animation();
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
        else if (collision.gameObject.CompareTag("Platform"))
            enPiso = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            enPiso = false;
        else if (collision.gameObject.CompareTag("Platform"))
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

    public void Animation(){
        if(direccionMovimiento == 0f)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
            
            // Voltear el sprite según la dirección
            if(direccionMovimiento < 0f)
            {
                spriteRenderer.flipX = true;
            }
            else if(direccionMovimiento > 0f)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}