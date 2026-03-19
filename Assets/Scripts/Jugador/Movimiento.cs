using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;

    [Header("Botones UI")]
    public Button botonIzquierdo;
    public Button botonDerecho;
    public Button botonSalto;

    [SerializeField] private Transform player;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool enPiso;
    private float direccionMovimiento = 0f;

   [SerializeField] private GameManager gameManager;
    private int NivelActual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        AddHoldListener(botonIzquierdo,  () => direccionMovimiento = -1f, () => direccionMovimiento = 0f);
        AddHoldListener(botonDerecho, () => direccionMovimiento =  1f, () => direccionMovimiento = 0f);

        botonSalto.onClick.AddListener(Jump);
        animator.SetBool("Radio", true);
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

        if (collision.gameObject.CompareTag("obstaculo"))
        {
            killPlayer();
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sensor"))
        {
            gameManager.CargarNivel();
        }
        if (other.CompareTag("Gusano"))
        {
            animator.SetBool("Radio", false);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("CheckPoint"))
        {
            NivelActual = SceneManager.GetActiveScene().buildIndex;
             PlayerPrefs.SetInt("NivelActual", NivelActual);
             Debug.Log("Nivel guardado: " + NivelActual);
        }


    }

    public void killPlayer()
    {
        Destroy(player.gameObject);
        Respawn();
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}