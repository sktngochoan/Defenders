using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    void Start()
    {
    }
    void Update()
    {
        movement();
    }
    // Test
    private void movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void onHitPlayer()
    {

    }
}