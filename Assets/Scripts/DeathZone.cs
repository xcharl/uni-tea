using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MainManager.Instance.RemoveLife();

        var paddle = GameObject.Find("Paddle");
        if (paddle == null)
        {
            throw new System.Exception("HHEHEE");
        }

        var paddleScript = paddle.GetComponent<Paddle>();
        paddleScript.AttachBallToPaddle();
        Destroy(other.gameObject);
    }
}
