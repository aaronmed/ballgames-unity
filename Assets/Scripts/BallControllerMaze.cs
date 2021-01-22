using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallControllerMaze : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    public Text WinText;
    private bool ballIsOnGround = true;
    public float speed;
    public Text countText;

    public GameObject end;
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
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z);

        rb.AddForce(movement * speed);

        if (Input.GetButtonDown("Jump") && ballIsOnGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            ballIsOnGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        count++;
        countText.text = "Count: " + count.ToString();
        if (count == 4)
        {
            end.SetActive(false);
        }
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            ballIsOnGround = true;
        }

        if (collision.gameObject.name == "Finish")
        {
            WinText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene(SceneManager.GetSceneByName("Scene3").buildIndex);
        }
    }
}
