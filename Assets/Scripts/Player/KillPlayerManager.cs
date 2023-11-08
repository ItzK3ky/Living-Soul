using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerManager : MonoBehaviour
{
    //Only called by event
    public void KillPlayer(Component sender, object data)
    {
        GameObject.Destroy(gameObject);
    }
}
