using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float minX, maxX, minY, maxY;
    void Update()
    {
        if(player != null)
        {
            Vector3 nextPos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
        }

        //Vector3 nextPos = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y + 0.59f, minY, maxY), transform.position.z);
        
    }
}
