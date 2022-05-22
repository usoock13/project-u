using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody playerRigidbody;
    float moveSpeed = 10f;
    Client networkClient;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody>();
        networkClient = GetComponent<Client>();
    }
    void Update() {
        BasicMove();
    }
    void BasicMove() {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.TransformDirection(new Vector3(dirX, 0, dirY)) * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + direction);
    }
}
