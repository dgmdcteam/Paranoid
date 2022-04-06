using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private Transform cam;
    private Vector3 scaleNormal, scaleCrouching;
    private GameObject rightHand;

    [SerializeField]
    [Tooltip("Player speed walking m/s")]
    [Range(0, 15)]
    private float _speedMovementWalk;

    [SerializeField]
    [Tooltip("Player speed running m/s")]
    [Range(15, 25)]
    private float _speedMovementRun;

    [SerializeField]//Revisar
    [Tooltip("Player rotation speed degrees/s")]
    [Range(0, 10)]
    private float _speedRotation;

    [SerializeField]
    [Tooltip("Player jump force N/m")]
    [Range(0, 5)]
    private float _jumpForce;

    [SerializeField]
    [Tooltip("Head rotation")]
    [Range(-50, 50)]
    private float _rotationHead;

    [Tooltip("Mouse rotation sensitivity")]
    [Range(5, 50)]
    private float _sensibilityMouse;

    private float _x, _y, _space, _speedMovement, angle, _mouseX, _mouseY;
    private bool _canJump, _crouching;

    public Animator Anim
    {
        get => anim;
        set => anim = value;
    }

    public float SpeedMovementWalk
    {
        get => _speedMovementWalk;
        set => _speedMovementWalk = value;
    }

    public float SpeedMovementRun
    {
        get => _speedMovementRun;
        set => _speedMovementRun = value;
    }

    public float SpeedRotation
    {
        get => _speedRotation;
        set => _speedRotation = value;
    }

    public float JumpForce
    {
        get => _jumpForce;
        set => _jumpForce = value;
    }

    public float RotationHead
    {
        get => _rotationHead;
        set => _rotationHead = value;
    }

    public float SensibilityMouse
    {
        get => _sensibilityMouse;
        set => _sensibilityMouse = value; //Esto esta aqui para ir probando, esto se eliminara en futuras actualizaciones
    }

    public bool CanJump
    {
        get => _canJump;
        set => _canJump = value;
    }

    public bool Crouching
    {
        get => _crouching;
        set => _crouching = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //cam = transform.GetChild(0);
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //rightHand = GameObject.Find("RightHand");
        InicialiacionValores();
    }

    // Update is called once per frame
    void Update()
    {
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X") * SensibilityMouse;
        _mouseY = Input.GetAxis("Mouse Y") * SensibilityMouse;
        Crouching = Input.GetKey(KeyCode.LeftControl) ? Crouching = true : Crouching = false;

        if (Crouching)
        {
            _speedMovement = 2f;
        }
        else
        {
            _speedMovement = Input.GetKey(KeyCode.LeftShift) ? SpeedMovementRun : SpeedMovementWalk;
        }
        _space = _speedMovement * Time.deltaTime;
        rb.velocity = transform.forward * _space * _y + transform.right * _space * _x + new Vector3(0, rb.velocity.y, 0);

        Vector3 dir = new Vector3(_x, 0, _y);
        transform.Translate(dir.normalized * _space);

        RotationHead -= _mouseY;
        transform.rotation *= Quaternion.Euler(0, _mouseX, 0);
        RotationHead = Mathf.Clamp(RotationHead, -50f, 50f);
        //cam.localRotation = Quaternion.Euler(RotationHead, 0, 0);
        //rightHand.transform.localRotation = Quaternion.Euler(RotationHead, 0, 0);

        transform.localScale = Vector3.Lerp(transform.localScale, Crouching ? scaleCrouching : scaleNormal, 0.1f);
    }

    private void InicialiacionValores()
    {
        scaleNormal = transform.localScale;
        scaleCrouching = scaleNormal;
        scaleCrouching.y = scaleNormal.y * 0.75f;
        SpeedMovementWalk = 3.0f;
        SpeedMovementRun = 5.0f;
        SpeedRotation = 8.0f;
        JumpForce = 5.0f;
        RotationHead = transform.rotation.x;
        SensibilityMouse = 5.0f;
        CanJump = false;
        Crouching = false;
    }
}
