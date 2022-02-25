using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    void Update()
    {
        this.transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, (player1.transform.position.y + player2.transform.position.y) / 2, -10);
    }
}
