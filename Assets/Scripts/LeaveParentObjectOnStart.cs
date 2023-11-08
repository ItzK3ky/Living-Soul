using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveParentObjectOnStart : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(null, true);
    }
}
