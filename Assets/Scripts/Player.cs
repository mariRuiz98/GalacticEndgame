using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private int maxHealth;

    private int currentHealth; 

    [SerializeField] private Image healthBarFill; // Referencia al fill de la barra de vida
    private SpriteRenderer spriteRenderer; // Para calcular el tamaño del jugador

    //public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
        // Inicializar la barra de salud llena (FillAmount = 1 equivale al 100%)
        healthBarFill.fillAmount = 1f;
        
        // Obtener el SpriteRenderer del jugador, como el sprite renderer está en el hijo de Player, se debe buscar en los hijos
        spriteRenderer = transform.Find("PlayerShips_5").GetComponent<SpriteRenderer>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update() {
        // Mover al jugador
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        //transform.Translate(new Vector3(inputH, inputV, 0).normalized * movementSpeed * Time.deltaTime, Space.World);
        Vector3 newPosition = transform.position + 
                              new Vector3(inputH, inputV, 0).normalized * movementSpeed * Time.deltaTime;

        // Limitar la posición del jugador dentro de los límites de la pantalla
        newPosition = ClampPositionToScreen(newPosition);

        // Aplicar la nueva posición
        transform.position = newPosition;
        
    }

    // Método para limitar la posición del jugador a los límites de la pantalla
    private Vector3 ClampPositionToScreen(Vector3 position)
    {
        // Obtener los bordes visibles de la cámara
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // Esquina inferior izquierda
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));  // Esquina superior derecha

        // Obtener el tamaño del jugador para evitar que salga parcialmente
        float playerWidth = spriteRenderer.bounds.extents.x;
        float playerHeight = spriteRenderer.bounds.extents.y;

        // Limitar la posición usando Mathf.Clamp
        position.x = Mathf.Clamp(position.x, bottomLeft.x + playerWidth, topRight.x - playerWidth);
        position.y = Mathf.Clamp(position.y, bottomLeft.y + playerHeight, topRight.y - playerHeight);

        return position;
    }

    // Método para restar una vida al jugador
    public void LoseLife(int damage)
    {
        currentHealth -= damage;

        // Asegurar que la vida no baje por debajo de 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de vida
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        Debug.Log("Vida actual: " + currentHealth);

        // Si las vidas llegan a 0 terminar el juego
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOver");
        }
    }
}
