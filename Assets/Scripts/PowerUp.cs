using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float timePowerUp;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PowerUp activo");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log($"Colisión detectada con: {other.gameObject.name}");
        // Si el asteroide colisiona con una bala
        if (other.CompareTag("Player"))
        {
            // Sumar puntos al GameManager cuando un asteroide colisiona con una bala
            Debug.Log("¡POWER UP!");
            
            ShootingSystem shootingSystem = other.GetComponent<ShootingSystem>();
            if(shootingSystem != null) {
                shootingSystem.EnableDoubleBullet(timePowerUp);
            }
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
