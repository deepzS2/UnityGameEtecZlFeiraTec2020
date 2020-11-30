using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comendo o player hmmmmmm
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
