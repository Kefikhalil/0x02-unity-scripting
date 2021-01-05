using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float gravityValue = -9.81f;
    public float playerSpeed = 2.0f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private int score = 0;
    public int health = 5;
    private Scene maze;
    private Rigidbody rb;

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        maze = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            health = 5;
            score = 0;
            SceneManager.LoadScene(maze.name);
        }
    }
    void FixedUpdate()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score++;
            Destroy(other.gameObject);
            Debug.Log($"Score: {score}");
        }
        if (other.tag == "Trap")
        {
            health--;
            Debug.Log($"Health: {health}");
        }
        if (other.tag == "Goal")
        {
            Debug.Log("You win!");
        }
    }
}
