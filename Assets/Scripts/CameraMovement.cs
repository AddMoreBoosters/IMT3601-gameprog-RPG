using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //  Player could have been destroyed
        if (player != null)
        {
            //  Camera follows the player
            transform.position = player.position + offset;
        }
    }
}
