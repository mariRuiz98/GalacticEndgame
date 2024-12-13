using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip destructionClip;
    [SerializeField] private GameObject powerUpPrefab;
    private AudioSource destructionSound;

    private GameManager gameManager;
    
    private int damage; 
    private bool isReleased = false; // Nueva bandera para evitar liberaciones múltiples

    private ObjectPool<Asteroid> myPool;
    
    public ObjectPool<Asteroid> MyPool
    {
        get => myPool;
        set => myPool = value;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnEnable()
    {
        isReleased = false; // Reiniciar la bandera al habilitar el asteroide
        GetComponent<Collider2D>().enabled = true; // Reactivar el collider
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Aquí se detectan las colisiones
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isReleased) return; // Evitar colisiones múltiples

        // Si el asteroide colisiona con una bala
        if (other.CompareTag("Bullet"))
        {
            // Sumar puntos al GameManager cuando un asteroide colisiona con una bala
            Debug.Log("¡Asteroide destruido!");
            
            // Reproducir el sonido de destrucción
            if (destructionSound != null)
            {
                Debug.Log("Reproduciendo sonido en la posición: " + transform.position);
                destructionSound.PlayOneShot(destructionClip);
            }

            float probability = Random.value;

            if (probability <= 0.3f)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            // Aumentar el puntaje del jugador
            gameManager.AddPoint(1);    

            // Liberar la bala al Object Pool
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null) bullet.MyPool.Release(bullet); 
            
            isReleased = true; // Marcar como liberado
            GetComponent<Collider2D>().enabled = false; // Desactivar el collider para evitar más colisiones //
            myPool.Release(this); // Liberar el asteroide
            //bullet.MyPool.Release(bullet); // Liberar la bala al Object Pool
            //myPool.Release(this);
        }
        
        // Si el asteroide colisiona con el jugador
        else if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.LoseLife(damage); // Restar una vida al jugador
            }
            myPool.Release(this);
        }
    }

    void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);

        // Verificar si el asteroide ha salido del borde izquierdo de la pantalla
        float leftBound = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        // Si el asteroide ha pasado el borde izquierdo de la pantalla
        if (transform.position.x < leftBound)
        {
            // Liberar el asteroide al Object Pool
            myPool.Release(this);
        }
    }
}
