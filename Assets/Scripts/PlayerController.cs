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
	private bool ifFriction;
	private bool ifSlippery;


    private Rigidbody2D rb;
    private int angle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        winText.text = "";
        angle = 0;
		ifFriction = false;
		ifSlippery = false;
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

		if (ifSlippery == true) {
			rb.drag = 0.01f;
		}
		else if (ifFriction == true) {

			rb.drag = 3;

		} else if (ifSlippery == false && ifFriction == false){
			rb.drag = 1f;
		}


        if (Input.GetKeyDown("space"))
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float moveHorizontal = Mathf.Sin(angleRad);
            float moveVertical = Mathf.Cos(angleRad);
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.AddForce(movement * speed );
        }

        if (sizeSlider.value == 0)
        {
            winText.text = "Level Cleared!";
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive (false);
			sizeSlider.value += 10;
		} else if (other.gameObject.CompareTag ("Getthin")) {
			other.gameObject.SetActive (false);
			sizeSlider.value -= 10;
		} else if (other.gameObject.CompareTag ("Friction")) {
			ifFriction = true;
			ifSlippery = false;
		} else if (other.gameObject.CompareTag ("NonFriction")) {
			ifFriction = false;
			ifSlippery = false;
		} else if (other.gameObject.CompareTag ("Slippery")) {
			ifSlippery = true;
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
