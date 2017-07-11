using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByGameOver : MonoBehaviour {

    private GameController gameController;


    // Use this for initialization
    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {

            gameController = gameControllerObject.GetComponent<GameController>();

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (gameController.gameOver)
        {

            Destroy(gameObject);
        }


    }
}


