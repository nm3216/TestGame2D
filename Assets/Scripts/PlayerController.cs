using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text winText;
    public Slider sizeSlider;
    public Texture dirBg;
    public Texture dirNeedle;
    public int needleSpeed;

    private Rigidbody2D rb;
    private int angle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        winText.text = "";
        angle = 0;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 120, 120), dirBg);
        Vector2 pivot = new Vector2(60, 60);
        GUIUtility.RotateAroundPivot(angle, pivot);
        GUI.DrawTexture(new Rect(0, 0, 120, 120), dirNeedle);
    }

    void FixedUpdate()
    {
        angle = (angle + needleSpeed) % 360;

        if (Input.GetKeyDown("space"))
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float moveHorizontal = Mathf.Sin(angleRad);
            float moveVertical = Mathf.Cos(angleRad);
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.AddForce(movement * speed);
        }

        if (sizeSlider.value == 0)
        {
            winText.text = "Level Cleared!";
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            sizeSlider.value += 10;
        }
        else if (other.gameObject.CompareTag("Getthin"))
        {
            other.gameObject.SetActive(false);
            sizeSlider.value -= 10;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;
        if (other.gameObject.CompareTag("Wall"))
        {
            sizeSlider.value -= 10;
        }
    }

}
