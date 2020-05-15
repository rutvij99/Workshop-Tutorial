using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Basic Variables")]
    public float speed;
    public float jumpSpeed;

    [Header("UI Componenets")]
    public GameObject scorePanel;
    public Text scoreUI;
    public GameObject menuPanel;
    public Text menuTextUI;

    private int score;
    private float jumpTimer;
    private bool isJumping;
    private bool inAir;
    private bool isGameComplete;
    private bool isPaused;
    private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isGameComplete = false;
        rigidBody = this.GetComponent<Rigidbody>();
        menuPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            PlayPause();
        }

        if (score >= 10)
        {
            isGameComplete = true;
            score = 10;
            scoreUI.text = score.ToString();
            menuPanel.SetActive(true);
            menuTextUI.text = "You Won...!";
        }
        else if(score < 0)
        {
            isGameComplete = true;
            score = 0;
            scoreUI.text = score.ToString();
            menuPanel.SetActive(true);
            menuTextUI.text = "You Lost...!";
        }
    }

    private void FixedUpdate()
    {
        if (!isGameComplete && !isPaused)
        {
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                isJumping = true;
                inAir = true;
            }
            if (inAir && jumpTimer < 1)
            {
                jumpTimer += 1 * Time.deltaTime;
                rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 3, Input.GetAxis("Vertical")) * jumpSpeed;
            }
            else if (isJumping)
            {
                inAir = false;
                jumpTimer = 0;
                rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), -6, Input.GetAxis("Vertical")) * jumpSpeed;
            }
            else
            {
                isJumping = false;
                jumpTimer = 0;
                rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            score++;
            scoreUI.text = score.ToString();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            score--;
            scoreUI.text = score.ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Land"))
            isJumping = false;
    }

    public void PlayPause()
    {
        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);
    }
}
