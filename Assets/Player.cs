using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int moveSpeed = 1;

    public enum Direction
    {
        Up,
        Left,
        Right,
        Down,
        COUNT
    }
    public Direction direction = Direction.Left;

    Vector2Int movement = Vector2Int.zero;

    RectTransform rt;
    RectTransform currentTrail;
    RectTransform previousTrail;

    Vector3 lastTurnPosition;

    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public GameObject trailPrefab;
    public GameObject trailsParent;

    public Material material;

    public bool ready = false;
    public bool alive = true;

	// Use this for initialization
	void Start ()
    {
        rt = GetComponent<RectTransform>();

        StartNewTrail();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (alive && GameController.Instance.gameState == GameController.GameState.Play)
        {
            HandleInput();

            switch (direction)
            {
                case Direction.Up:
                    movement = new Vector2Int(0, moveSpeed);
                    break;

                case Direction.Left:
                    movement = new Vector2Int(-moveSpeed, 0);
                    break;

                case Direction.Right:
                    movement = new Vector2Int(moveSpeed, 0);
                    break;

                case Direction.Down:
                    movement = new Vector2Int(0, -moveSpeed);
                    break;
            }
            rt.localPosition = new Vector3(rt.localPosition.x + movement.x * Time.deltaTime, rt.localPosition.y + movement.y * Time.deltaTime, 0);

            LayTrail();
        }
        else if (alive && GameController.Instance.gameState == GameController.GameState.WaitingForReady)
        {
            HandleReadyInput();
        }
    }

    void HandleReadyInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ready = true;
        }
    }

    public void ResetPlayer()
    {
        ready = false;
        alive = true;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(up) && direction != Direction.Down)
        {
            direction = Direction.Up;
            StartNewTrail();
        }
        else if (Input.GetKeyDown(down) && direction != Direction.Up)
        {
            direction = Direction.Down;
            StartNewTrail();
        }
        else if (Input.GetKeyDown(left) && direction != Direction.Right)
        {
            direction = Direction.Left;
            StartNewTrail();
        }
        else if (Input.GetKeyDown(right) && direction != Direction.Left)
        {
            direction = Direction.Right;
            StartNewTrail();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (previousTrail && currentTrail)
        {
            if (col.gameObject != previousTrail.gameObject && col.gameObject != currentTrail.gameObject)
            {
                GameController.Instance.gameplayCanvas.ShowParticles(rt.localPosition, 5);
                GameController.Instance.gameplayCanvas.Shake();

                alive = false;
            }
        }
    }

    void StartNewTrail()
    {
        // -- make sure we update our current trail
        if (currentTrail)
        {
            LayTrail();
        }

        previousTrail = currentTrail;

        // -- create new trail go and get its recttransform
        currentTrail = GameObject.Instantiate(trailPrefab, trailsParent.transform).GetComponent<RectTransform>();
        currentTrail.localPosition = rt.localPosition;

        // set the color
        currentTrail.GetComponent<Image>().material = material;

        // -- rotate and do position offset to have nice corners
        switch (direction)
        {
            case Direction.Up:
                currentTrail.transform.rotation = Quaternion.Euler(0, 0, 90);
                currentTrail.transform.localPosition = currentTrail.transform.localPosition + new Vector3(0, -0.5f, 0);
                break;

            case Direction.Down:
                currentTrail.transform.rotation = Quaternion.Euler(0, 0, 270);
                currentTrail.transform.localPosition = currentTrail.transform.localPosition + new Vector3(0, 0.5f, 0);
                break;

            case Direction.Left:
                currentTrail.transform.rotation = Quaternion.Euler(0, 0, 180);
                currentTrail.transform.localPosition = currentTrail.transform.localPosition + new Vector3(0.5f, 0,0);
                break;

            case Direction.Right:
                currentTrail.transform.rotation = Quaternion.Euler(0, 0, 0);
                currentTrail.transform.localPosition = currentTrail.transform.localPosition + new Vector3(-0.5f,0,0);
                break;
        }

        lastTurnPosition = rt.localPosition;
    }

    void LayTrail()
    {
        float distance = Vector3.Distance(lastTurnPosition, rt.localPosition);

        currentTrail.sizeDelta = new Vector2(distance,2);
        
        currentTrail.GetComponent<BoxCollider2D>().size = new Vector2(distance,2);
        currentTrail.GetComponent<BoxCollider2D>().offset = new Vector2((distance / 2), 0);
    }
}
