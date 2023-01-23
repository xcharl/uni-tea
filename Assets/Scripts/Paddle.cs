using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float PaddleSpeed = 200f;
    public GameObject ballPrefab;

    private new Collider collider;
    private GameObject ball;
    private bool isBallReleased;

    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<Collider>();

        if (this.ballPrefab == null)
        {
            throw new Exception("no ballprefab on paddle");
        }

        AttachBallToPaddle();
    }

    public void AttachBallToPaddle()
    {
        var ballPosition = this.transform.position + new Vector3(0, 1f, 0);
        this.ball = Instantiate(this.ballPrefab, ballPosition, Quaternion.identity);
        this.isBallReleased = false;
        this.ball.transform.SetParent(this.transform);
        this.ball.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        var xForce = PaddleSpeed * Input.GetAxis("Horizontal");
        this.GetComponent<Rigidbody>().AddForce(xForce * this.GetComponent<Rigidbody>().mass, 0, 0);

        if (!isBallReleased && Input.GetKeyDown(KeyCode.Space))
        {
            this.ReleaseBall();
        }
    }

    private void ReleaseBall()
    {
        this.isBallReleased = true;
        this.ball.GetComponent<Rigidbody>().isKinematic = false;
        this.ball.GetComponent<Rigidbody>().AddForce(0, 300f, 0);
        this.ball.transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider == this.collider)
            {
                var positionOnPaddle = contact.point.x - transform.position.x;
                var otherRigidbody = contact.otherCollider.GetComponent<Rigidbody>();
                if (otherRigidbody != null)
                {
                    otherRigidbody.AddForce(300 * positionOnPaddle, 0, 0);
                }
            }
        }
    }
}
