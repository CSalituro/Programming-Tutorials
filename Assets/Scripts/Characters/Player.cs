using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayers;

    private Vector2 currentRotation;
    
    [Header("Camera")]
    [SerializeField, Range(1, 20)] private float mouseSensX;
    [SerializeField, Range(1, 20)] private float mouseSensY;
    
    [SerializeField, Range(-90, 0)] private float minViewAngle;
    [SerializeField, Range(0, 90)] private float maxViewAngle;

    [SerializeField] private Transform followTarget;
    
    [Header("Shooting")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float projectileForce;

    [Header("Player UI")] 
    [SerializeField] private Image healthbar;
    [SerializeField] private TextMeshProUGUI shotsFired;

    [SerializeField] private float maxHealth;
    private int shotsFiredCounter;
    private float _health;

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthbar.fillAmount = _health / maxHealth;
        }
    }
    
    

    private Vector2 currentAngle;

    private bool isGrounded;
    private Vector3 _moveDirection;
    
    private Rigidbody rb;
    private float depth;
    
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(myPlayer:this);
        InputManager.SetGameControls();

        rb = GetComponent<Rigidbody>();
        depth = GetComponent<Collider>().bounds.size.y;

        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        CheckGround();

        Health -= Time.deltaTime * 5;
    }

    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void Jump()
    {
        Debug.Log("Jump called");
        if (isGrounded)
        {
            Debug.Log("I jumped");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, depth, groundLayers);
        Debug.DrawRay(transform.position, Vector3.down * depth, Color.green, 0, false);
    }

    public void SetLookRotatiom(Vector2 readValue)
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensY;

        currentAngle.y = Mathf.Clamp(currentAngle.y, minViewAngle, maxViewAngle);
        
        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    }

    public void Shoot()
    {
        Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        currentProjectile.AddForce(followTarget.forward * projectileForce, ForceMode.Impulse);

        ++shotsFiredCounter;

        shotsFired.text = shotsFiredCounter.ToString();
        
        Destroy(currentProjectile.gameObject,4);
    }
}
