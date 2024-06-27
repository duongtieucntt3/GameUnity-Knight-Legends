using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallScript : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform Player;
    public AudioSource respawn;
    public GameObject gameOver;
    public AudioSource background;
    public AudioSource backgroundDead;

    protected string sceneName = "MainMenu";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            respawn.Play();

            Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, 0f);
        }
    }
    public void Respawn()
    {
        respawn.Play();
        Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, 0f);
        FindObjectOfType<HealthController>().Resurrect();
        background.Play();
        backgroundDead.Stop();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(this.sceneName);
    }
}
