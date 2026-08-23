using System.Collections;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DelayedCam : MonoBehaviour // Camera slowly follows the player
{
    public GameObject player;
    public float speed = 1.0f;
    public int zDistance = 10; // The z distance between this object and the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null)
        {
            player.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {                                       //Start pos       end pos                    speed
            transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - zDistance); // Keep the camera at a fixed z distance from the player
        }
    }
}
