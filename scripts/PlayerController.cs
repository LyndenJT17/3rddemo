using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public List<string> items;
	public ProjectileGunTutorial gunStats;

	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	public float gravity = -9.8f;
	public float jumpHeight = 3f;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	bool isGrounded;
	Vector3 velocity;

	public Rigidbody playerBody;

	Transform cameraT;

	void Start()
	{
		items = new List<string>();
		cameraT = Camera.main.transform;
	}

	void Update()
	{

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		bool running = Input.GetKey(KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			Debug.Log("Jump Pressed");
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
		velocity.y += gravity * Time.deltaTime;
		playerBody.velocity = velocity;
		//controller.Move(velocity * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
			string itemType = other.gameObject.GetComponent<ItemCollectable>().itemType;
			items.Add(itemType);
			Destroy(other.gameObject);

			gunStats.gunUpgrade();
        }
    }

}