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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        Debug.Log("¡dentro POWER UP!");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
