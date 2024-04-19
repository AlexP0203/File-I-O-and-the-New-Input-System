using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

public class FPSController : MonoBehaviour
{
    // references
    CharacterController controller;
    [SerializeField] GameObject cam;
    [SerializeField] Transform gunHold;
    [SerializeField] Gun initialGun;
    [SerializeField] UnityEvent onHit;

    // stats
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float lookSensitivityX = 1.0f;
    [SerializeField] float lookSensitivityY = 1.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 10;

    // private variables
    Vector3 velocity;
    bool grounded;
    float xRotation;
    List<Gun> equippedGuns = new List<Gun>();
    int gunIndex = 0;
    Gun currentGun = null;
    bool playerShot = false;
    bool playerSprint = false;
    Vector2 playerMovement;
    Vector2 playerLook;

    // properties
    public GameObject Cam { get { return cam; } }


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // start with a gun
        AddGun(initialGun);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();
        FireGun();

        // always go back to "no velocity"
        // "velocity" is for movement speed that we gain in addition to our movement (falling, knockback, etc.)
        Vector3 noVelocity = new Vector3(0, velocity.y, 0);
        velocity = Vector3.Lerp(velocity, noVelocity, 5 * Time.deltaTime);
    }

    void Movement()
    {
        grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        Vector2 movement = playerMovement;
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(move * movementSpeed * (playerSprint ? 2 : 1) * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Look()
    {
        Vector2 looking = playerLook;
        float lookX = looking.x * lookSensitivityX * Time.deltaTime;
        float lookY = looking.y * lookSensitivityY * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * lookX);
    }

    void FireGun()
    {
        if (playerShot)
        {
            currentGun?.AttemptFire();
        }
        playerShot = false;
    }

    void EquipGun(Gun g)
    {
        // disable current gun, if there is one
        currentGun?.gameObject.SetActive(false);

        g.gameObject.SetActive(true);
        g.transform.parent = gunHold;
        g.transform.localPosition = Vector3.zero;
        currentGun = g;
    }

    // public methods

    public void AddGun(Gun g)
    {
        // add new gun to the list
        equippedGuns.Add(g);

        // our index is the last one/new one
        gunIndex = equippedGuns.Count - 1;

        // put gun in the right place
        EquipGun(g);
    }

    public void IncreaseAmmo(int amount)
    {
        currentGun.AddAmmo(amount);
    }

    // Input methods

    /*bool GetPressFire()
    {
        return Input.GetButtonDown("Fire1");
    }

    bool GetHoldFire()
    {
        return Input.GetButton("Fire1");
    }

    Vector2 GetPlayerMovementVector()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    Vector2 GetPlayerLook()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    bool GetSprint()
    {
        return Input.GetButton("Sprint");
    }*/

    // Collision methods

    // Character Controller can't use OnCollisionEnter :D thanks Unity
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Damager>())
        {
            var collisionPoint = hit.collider.ClosestPoint(transform.position);
            var knockbackAngle = (transform.position - collisionPoint).normalized;
            velocity = (20 * knockbackAngle);
            onHit?.Invoke();
        }
    }

    public void OnMovement(InputValue value)
    {
       playerMovement = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        playerLook = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (grounded)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -1 * gravity);
        }
    }
    void OnSprint()
    {
        playerSprint = !playerSprint;
    }

    void OnShoot()
    {
        playerShot = true;
    }

    public void DisablePlayerController()
    {
        if(controller.enabled == false)
        {
            controller.enabled = true;
        }
        else
        {
            controller.enabled = false;
        }
    }
}
