using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Stone : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameManager gameManager;

    private ObjectPool<Stone> myPool;
    
    public ObjectPool<Stone> MyPool
    {
        get => myPool;
        set => myPool = value;
    }
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el asteroide colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Se ha obtenido una Piedra del Infinito!");
            gameManager.AddStone(1); // Aumentar las piedras obtenidas por el jugador
            myPool.Release(this);
        }
    }

    void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);

        // Si la piedra ha pasado el borde izquierdo de la pantalla
        float leftBound = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        if (transform.position.x < leftBound)
        {
            // Liberar la piedra al Object Pool
            myPool.Release(this);
        }
    }
}
