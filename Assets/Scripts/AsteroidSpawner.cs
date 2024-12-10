using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    private float spawnRate = 2;  // Frecuencia de aparición de asteroides
    private float minAsteroid = 5; //Número mínimo de asteroides que deben haber en pantalla
    
    float minSpeed = 1; // Velocidad mínima de los asteroides
    float maxSpeed = 10; // Velocidad máxima de los asteroides
    private float timer;
    private ObjectPool<Asteroid> asteroidPool;
    private List<Asteroid> activeAsteroids = new List<Asteroid>();  // Lista de asteroides activos para llevar el control de cuántos asteroides hay en pantalla

    [SerializeField] private float spawnHeightRange = 5f; // Rango vertical
    [SerializeField] private float minScale = 0.5f; // Tamaño mínimo
    [SerializeField] private float maxScale = 2f;   // Tamaño máximo


    private void Awake() 
    {
        asteroidPool = new ObjectPool<Asteroid>(CreateAsteroid, GetAsteroid, ReleaseAsteroid, DestroyAsteroid);
    }

    private Asteroid CreateAsteroid()
    {
        Asteroid asteroidCopy = Instantiate(asteroidPrefab, transform.position, Quaternion.identity); 
        asteroidCopy.MyPool = asteroidPool; // Asignar el pool al asteroide
        return asteroidCopy;
    }

    private void GetAsteroid(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(true);
        activeAsteroids.Add(asteroid);  // Agregar el asteroide a la lista de asteroides activos 
          
        // Posición de aparición del asteroide: detrás del lateral derecho de la cámara
        float spawnX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + 1; // Fuera de la vista izquierda
        float spawnY = UnityEngine.Random.Range(-spawnHeightRange, spawnHeightRange); // Posición vertical aleatoria
        asteroid.transform.position = new Vector3(spawnX, spawnY, 0);
        // asteroid.transform.position = transform.position; // Posicionar el objeto en la posición del eje X izquierdo

        // Tamaño del asteroide aleatorio 
        float scale = UnityEngine.Random.Range(minScale, maxScale);
        asteroid.transform.localScale = new Vector3(scale, scale, scale);

        // Ajustar velocidad del asteroide según su tamaño
        float speed = asteroid.GetSpeed();
        //float newSpeed = speed / asteroid.transform.localScale.magnitude; // Magnitud calcula el tamaño total.
        float newSpeed = Mathf.Clamp(speed / asteroid.transform.localScale.x, minSpeed, maxSpeed);
        asteroid.SetSpeed(newSpeed); 
        
        // Ajustar el daño del asteroide según su tamaño
        int newDamage;
        if (transform.localScale.x < 1.5f) // Umbral para "pequeño"
        {
            newDamage = 10; // Daño del asteroide pequeño
        }
        else
        {
            newDamage = 20; // Daño del asteroide grande
        }
        asteroid.SetDamage(newDamage);

    }

    private void ReleaseAsteroid(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
        activeAsteroids.Remove(asteroid); // Quitar el asteroide de la lista de asteroides activos 
    }

    private void DestroyAsteroid(Asteroid asteroid)
    {
        Destroy(asteroid.gameObject);
    }

    void Start()
    {
        
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate && activeAsteroids.Count < minAsteroid) {
            timer = 0;
            asteroidPool.Get();
        }
    }
}
