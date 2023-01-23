using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour
{
    void Start()
    {
        MainManager.Instance.BricksCount++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MainManager.Instance.AddScore(100);
        MainManager.Instance.BricksCount--;
        Destroy(this.gameObject);
        MainManager.Instance.ProgressIfWon();
    }

    private void OnDestroy()
    {
    }
}
