using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Salut");
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
