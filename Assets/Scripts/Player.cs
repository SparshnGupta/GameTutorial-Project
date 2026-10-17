using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool JumpKeyWasPressed;
    float HorizontalInput;
    Rigidbody RigidbodyComponent;
    int SuperJumpRemaining;
    [SerializeField] Transform GroundCheckTransform;
    [SerializeField] LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        RigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpKeyWasPressed = true;
        }

        HorizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        RigidbodyComponent.linearVelocity = new Vector3(HorizontalInput, RigidbodyComponent.linearVelocity.y, 0);

        if (Physics.OverlapSphere(GroundCheckTransform.position, 0.1f,playerMask).Length == 0)
        {
            return;
        }

        if (JumpKeyWasPressed)
        {
            float JumpPower = 5f;
            if (SuperJumpRemaining > 0)
            {
                JumpPower *= 2;
                SuperJumpRemaining--;
            }
            RigidbodyComponent.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
            JumpKeyWasPressed = false;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            SuperJumpRemaining++;
        }
    }

}
