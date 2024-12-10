using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private Stone stonePrefab;
    [SerializeField] private float spawnHeightRange = 5f; // Rango vertical
    private float spawnRate = 2;  // Frecuencia de aparición de Piedras del Infinito

    private float timer;
    private ObjectPool<Stone> stonePool;


    private void Awake() 
    {
        stonePool = new ObjectPool<Stone>(CreateStone, GetStone, ReleaseStone, DestroyStone);
    }

    private Stone CreateStone()
    {
        Stone stoneCopy = Instantiate(stonePrefab, transform.position, Quaternion.identity); 
        stoneCopy.MyPool = stonePool; // Asignar el pool a la piedra
        return stoneCopy;
    }

    private void GetStone(Stone stone)
    {
        stone.gameObject.SetActive(true);
        
        // Posición de aparición de la piedra: detrás del lateral derecho de la cámara
        float spawnX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + 1; // Fuera de la vista izquierda
        float spawnY = UnityEngine.Random.Range(-spawnHeightRange, spawnHeightRange); // Posición vertical aleatoria
        stone.transform.position = new Vector3(spawnX, spawnY, 0);
    }

    private void ReleaseStone(Stone stone)
    {
        stone.gameObject.SetActive(false);
    }

    private void DestroyStone(Stone stone)
    {
        Destroy(stone.gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate) {
            timer = 0;
            stonePool.Get();
        }
    }
}
