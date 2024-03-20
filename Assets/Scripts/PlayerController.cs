using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private int playerHealth = 3;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI playerHpText;
    public GameObject pickUpPrefab;
    public float timeRemaining = 60f;
    public float proximityDistance = 5f;
    public AudioSource smashSound;
    public AudioSource hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        SetPlayerHpText();

        // Start the game by generating the first "PickUp".
        GeneratePickUp();
        GeneratePickUp();
        GeneratePickUp();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // Update the time counter.
        UpdateTime();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            timeRemaining += 5f;
            GeneratePickUp();
            if (smashSound != null)
            {
                smashSound.Play();
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            playerHealth--;
            SetPlayerHpText();
            if (hitSound != null)
            {
                hitSound.Play();
            }
            if (playerHealth <= 0)
            {
                // Set the reason for defeat and final score before loading the Game Over scene
                PlayerPrefs.SetString("GameOverReason", "You ran out of health!");
                PlayerPrefs.SetInt("FinalScore", count);
                SceneManager.LoadScene(2);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void SetPlayerHpText()
    {
        if (playerHpText != null) // Add this check to avoid NullReferenceException
        {
            playerHpText.text = "HP: " + playerHealth.ToString();
        }
    }

    void GeneratePickUp()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-9f, 9f), 0f, Random.Range(-9f, 9f));
        GameObject newPickUp = Instantiate(pickUpPrefab, randomPosition, Quaternion.identity);

        PickUpMovement pickUpMovement = newPickUp.GetComponent<PickUpMovement>();
        if (pickUpMovement != null)
        {
            pickUpMovement.SetPlayerTransform(transform);
            pickUpMovement.proximityDistance = proximityDistance;
        }
    }

    void UpdateTime()
    {
        timeRemaining -= Time.deltaTime;
        timeText.text = "Time: " + Mathf.Max(0, Mathf.Ceil(timeRemaining)).ToString("0");
        if (timeRemaining <= 0)
        {
            
            PlayerPrefs.SetString("GameOverReason", "You ran out of time!");
            PlayerPrefs.SetInt("FinalScore", count);
            SceneManager.LoadScene(2);
        }
    }
}
