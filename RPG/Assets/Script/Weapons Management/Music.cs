using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Music : MonoBehaviour
{
    public UnityEvent misuc;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            misuc.Invoke();
        }
    }
}
