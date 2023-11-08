using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] private GameEvent killPlayerEvent;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void KillPlayer()
    {
        killPlayerEvent.Raise(this, null);
    }
}
