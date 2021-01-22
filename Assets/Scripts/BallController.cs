using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;

    public Text WinText;
    private bool ballIsOnGround = true;
    public float speed;
    public Text countText;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Count: " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z);

        rb.AddForce(movement * speed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && ballIsOnGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            ballIsOnGround = false;
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        count++;
        countText.text = "Count: " + count.ToString();
        if (count == 6)
        {
            WinText.gameObject.SetActive(true);
            yield return new WaitForSeconds (2.0f);
            SceneManager.LoadScene(SceneManager.GetSceneByName("Scene2").buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane" || collision.gameObject.name == "Obstacle")
        {
            ballIsOnGround = true;
            
        }
    }

}
