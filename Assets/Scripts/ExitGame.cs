using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // El AudioSource del botón que se va a clickear
    [SerializeField] private AudioClip clickSound;     // El clip de sonido a reproducir    

    public void Exit() {
        // Iniciar la corutina para reproducir el sonido y luego salir del juego
        StartCoroutine(PlaySoundAndExit());
    }

    private IEnumerator PlaySoundAndExit()
    {
        // Reproducir el sonido
        audioSource.PlayOneShot(clickSound);

        // Esperar a que el sonido termine (usamos el tiempo del clip de audio)
        yield return new WaitForSeconds(clickSound.length);

        // Salir del juego
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
