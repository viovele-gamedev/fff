using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    Quaternion StartingRotation;
    bool isGround;
    float Ver, Hor, Jump, RotHor, RotVer;
    [Header("Скорость")]
    public float speed;
    [Header("Прыжок")]
    public float JumpSpeed;
    [Header("Чувствительность")]
    public float Sensivity;
    private void Start()
    {
        StartingRotation = transform.rotation;
    }
    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGround = false;
        }
    }
    private void FixedUpdate()
    {
        RotHor += Input.GetAxis("Mouse X") * Sensivity;
        RotVer += Input.GetAxis("Mouse Y") * Sensivity;
        RotVer = Mathf.Clamp(RotVer, -60, 60);
        Quaternion RotY = Quaternion.AngleAxis(RotHor, Vector3.up);
        Quaternion RotX = Quaternion.AngleAxis(-RotVer, Vector3.right);
        cam.transform.rotation = StartingRotation * transform.rotation * RotX;
        transform.rotation = StartingRotation * RotY * RotX;
        transform.rotation = StartingRotation * RotY;
        if (isGround)
        {

            Ver = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;
            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(Hor, 0, Ver));
    }
}
