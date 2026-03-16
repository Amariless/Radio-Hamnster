using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private Transform player;

    private static Health instance { get; set; }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (player == null)
        {
            player = transform;
        }

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            killPlayer();
        }
    }

    public void killPlayer()
    {
        currentHealth = 0;
        Destroy(player.gameObject);
        Respawn();
        Debug.Log("Player muerto");
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Player respawned");
    }
}
