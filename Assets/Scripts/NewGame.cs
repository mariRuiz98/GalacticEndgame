using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // El AudioSource del botón que se va a clickear
    [SerializeField] private AudioClip clickSound;     // El clip de sonido a reproducir
    [SerializeField] private string sceneToLoad;

    public void StartNewGame() {
        // Iniciar la corutina para reproducir el sonido y luego cargar la escena
        Debug.Log("Iniciando nuevo juego...");
        StartCoroutine(PlaySoundAndLoadScene());
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        // Reproducir el sonido
        audioSource.PlayOneShot(clickSound);

        // Esperar a que el sonido termine (usamos el tiempo del clip de audio)
        yield return new WaitForSeconds(clickSound.length);

        // Cargar la nueva escena
        SceneManager.LoadScene(sceneToLoad);
    }
}
