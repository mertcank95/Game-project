using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Diamond"))
            Destroy(collision.gameObject);
    }
}
