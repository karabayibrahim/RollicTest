using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IPlatformCollect
{
    // Start is called before the first frame update
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    [SerializeField] private ControlType controlType;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float _movementClampNegative;
    [SerializeField] private float _movementClampPositive;
    [SerializeField] private float swerveSpeed;
    [SerializeField] private float moveSpeed;
    private Rigidbody rb;

    

    public float MoveFactorX => _moveFactorX;

    public float MoveSpeed 
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            if (MoveSpeed==value)
            {
                return;
            }
            moveSpeed = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState==GameState.PLAY)
        {
            if (controlType == ControlType.MOBILE)
            {
                MobileControl();
            }
            else
            {
                MovementInput();
            }
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

        

    }


    private void MovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
        }
        float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
        transform.Translate(0, 0, swerveAmount);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, _movementClampNegative, _movementClampPositive));
    }

    private void MobileControl()
    {
        if (Input.touchCount > 0)
        {
            Touch _theTouch = Input.GetTouch(0);

            if (_theTouch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = _theTouch.deltaPosition;
                if (touchPos != Vector2.zero)
                {
                    //transform.Translate(0, 0, touchPos.x * (horizontalSpeed / 100) * Time.fixedDeltaTime);
                    transform.Translate(0, 0, touchPos.x * (horizontalSpeed / 100) * Time.fixedDeltaTime);
                    //swerveSpeed = touchPos.x;
                    //transform.position += new Vector3(0, 0, touchPos.x * (horizontalSpeed / 100) * Time.deltaTime);
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, _movementClampNegative, _movementClampPositive));
                }
            }
        }

    }

    private void VerticalMove()
    {
        var x = (moveSpeed * Time.fixedDeltaTime) * -1;
        transform.Translate(new Vector3(x, 0, 0));
        //var newpos = transform.position + new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        //rb.MovePosition(newpos);
    }



    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState==GameState.PLAY)
        {
            VerticalMove();
        }
    }

    public void DoPlatformCollect()
    {
        GameManager.Instance.GameState = GameState.CALCULATE;
    }
}




