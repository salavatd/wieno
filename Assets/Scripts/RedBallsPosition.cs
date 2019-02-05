using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallsPosition : MonoBehaviour
{
    public float speed;
    public Vector3 BallDefault;
    public static bool Started;

    void Allow()
    {
        Started = false;
    }
    void Update()
    {
        if (Started)
        {
            transform.position = BallDefault;
            GetComponent<Rigidbody>().Sleep();
            Started = false;
        }
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speed, ForceMode.Force);
        }
        if (Input.GetKey (KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * speed, ForceMode.Force);
        }
        if (Input.GetKey (KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * speed, ForceMode.Force);
        }
        if (Input.GetKey (KeyCode.DownArrow))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * speed, ForceMode.Force);
        }
#endif
#if UNITY_ANDROID
        GetComponent<Rigidbody>().AddForce(Input.acceleration.x * speed, Input.acceleration.y * speed, 0, ForceMode.Force);
#endif
    }
    private void OnCollisionEnter(Collision Box)
    {
        if (Box.gameObject.tag == "Finish")
        {
            GameControl.FailGame = true;
        }
    }
}
