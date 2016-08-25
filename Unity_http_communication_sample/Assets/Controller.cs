using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	float speed = 6.0F;
	float jumpSpeed = 10.0F;
	float gravity = 20.0F;

    bool jumpBool = false;

	private Vector3 moveDirection = Vector3.zero;

	void Start(){
	}

	void Update(){
		CharacterController controller = GetComponent<CharacterController> ();

		if (controller.isGrounded) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

			moveDirection = transform.TransformDirection (moveDirection);

			moveDirection *= speed;

            if (Input.GetButton("Jump") || jumpBool)
            {
                moveDirection.y = jumpSpeed;
                jumpBool = false;
            }
		}

		moveDirection.y -= gravity * Time.deltaTime;

		controller.Move (moveDirection * Time.deltaTime);
	}

    public void jump()
    {
        jumpBool = true;
    }
}
