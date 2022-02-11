using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollerPlayer : MonoBehaviour, IDestructable
{
    [SerializeField] float maxForce = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] ForceMode forceMode;
    [SerializeField] Transform viewTransform;

    Rigidbody rb;
    Vector3 force = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        //World Space
        force = direction * maxForce;

        //View Space
        Quaternion viewSpace = Quaternion.AngleAxis(viewTransform.rotation.eulerAngles.y, Vector3.up);
        force = viewSpace * force;

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        BallGameManager.Instance.playerHealth = GetComponent<Health>().health;
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }

    public void Destroyed()
    {
        BallGameManager.Instance.OnPlayerDead();
    }
}
