using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -- Makes sure a gamecontroller instance is spawned by referencing it.
public class GameControllerSpawner : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        if (GameController.Instance != null)
        {
            Destroy(gameObject);
        }
	}
}
