using System.Collections;
using DG.Tweening;
using Managers;
using UnityEngine;

public class PaperPlaneController : MonoBehaviour
{
    
    #region Variables

    private FloatingJoystick _floatingJoystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeedNormal;
    [SerializeField] private float rotateSpeedSpining;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float thrustSpeed = 7f;
    [SerializeField] private float downwardThrust = 7f;
    [SerializeField] private float planeZRot = 15f;
    [SerializeField] private float clampValue;
    [SerializeField] private int initialThrustMultiplier = 3;    
    [SerializeField] private float timeForBoostedSpeed;
    private UIManager _uiManager;
    private Rigidbody _rigidbody;
    private float _horizontal;
    private bool _applyDownwardThrust;
    private bool _applyUpwardThrust;
    private bool _hasReachedEnd;
    private bool _isApplyingSwing;
    private float _curSpeed;
    private float _xRotateVal;
    private float _curThrust;

    #endregion

   

    private void Awake()
    {
        _floatingJoystick = FindObjectOfType<FloatingJoystick>();
        _rigidbody = GetComponent<Rigidbody>(); 
        _curSpeed = 0;
    }

    private void Start()
    {
        _xRotateVal = 0;
        _uiManager=UIManager.Instance;
        _floatingJoystick.gameObject.SetActive(false);
        EventsManager.ONGameStart += GiveInitialThrust;
        EventsManager.ONPassThroughHoop += IncreaseSpeed;
        EventsManager.ONCollisionWithObstacle += DecreaseSpeed;
        EventsManager.ONCollisionWithFan += GiveFanUpwardThrust;
        EventsManager.ONGameWin += DisableThrust;
        EventsManager.ONGameLose += DisableThrust;
    }

    private void Update()
    {
        HandleInputs();
        ClampAxis();
        HandleHorizontalMovement();
        RotatePlaneModel();
    }

    private void FixedUpdate()
    {
        if (_hasReachedEnd) return;

        if (_applyDownwardThrust)
        {
            _rigidbody.AddForce(transform.up * (-downwardThrust));
        }

        if (_applyUpwardThrust)
        {
            _rigidbody.AddRelativeForce(transform.forward * _curSpeed,ForceMode.Force);
        }
    }

    private void RotatePlaneModel()
    {
        var eulerAngles = transform.localEulerAngles;
        var rotation = transform.localRotation;
        _xRotateVal = Mathf.Clamp(_xRotateVal, -30, 30);
        if (_isApplyingSwing)
        {
            var rotationDuringHighSpeed = Quaternion.Euler(0, 0, eulerAngles.z+10f);
            transform.localRotation = Quaternion.Lerp(rotation, rotationDuringHighSpeed, Time.deltaTime * rotateSpeedSpining);
        }
        else
        {
            var normalRotation = Quaternion.Euler(_xRotateVal, 0, -_horizontal * planeZRot);
            transform.localRotation =Quaternion.Lerp(rotation, normalRotation, Time.deltaTime * rotateSpeedNormal);
        }
    }
    void HandleInputs()
    {
        _horizontal = _floatingJoystick.Horizontal;
      
    }

    private void HandleHorizontalMovement()
    {
        if(_hasReachedEnd) return;

        Transform transform1;
        (transform1 = transform).Translate(Vector3.right * (_horizontal * horizontalSpeed *Time.deltaTime),Space.World);
       transform.Translate(Vector3.forward*(_curSpeed*Time.deltaTime));
    }

    private void ClampAxis()
    {
        var localPosition = transform.localPosition;
        localPosition = new Vector3(Mathf.Clamp(localPosition.x, -clampValue, clampValue),
            localPosition.y, localPosition.z);
        transform.localPosition = localPosition;
    }

    #region EventCallBacks

    private void GiveInitialThrust()
    {
        _applyDownwardThrust = true;
        _curSpeed = moveSpeed;
        _floatingJoystick.gameObject.SetActive(true);
        float thrust = thrustSpeed;
        thrust +=  (thrust * _uiManager.GETPowerValue() )*initialThrustMultiplier ;
        print(thrust);
        GiveThrustOf(thrust);
    }

    private void IncreaseSpeed()
    {
        if(_hasReachedEnd) return;
        
        _applyDownwardThrust = false;
        _isApplyingSwing = true;
        var speed = _curSpeed;
        _curSpeed += 10f;
        StartCoroutine(nameof(BoostSpeedCounter),speed);
    }

    private void DecreaseSpeed()
    {
        if(_hasReachedEnd) return;
        
        if (_isApplyingSwing)
        {
            _isApplyingSwing = false;
            _applyDownwardThrust = true;
            
        }
        _curSpeed = moveSpeed;
        
        
        _rigidbody.AddForce(transform.up * (-thrustSpeed/4),ForceMode.Force);
        _xRotateVal = 40f;
        StartCoroutine(nameof(NormalizeRotation),.5);
    }
    private void GiveFanUpwardThrust()
    {
        if(_hasReachedEnd) return;
     
        _applyDownwardThrust = false;
        _applyUpwardThrust = true;
        _curSpeed += 1f;
        _xRotateVal -= 20;

        DOTween.To(() => _curThrust, x => _curThrust = x, thrustSpeed, .75f).OnComplete(() =>
        {
            _applyDownwardThrust = true;
            _applyUpwardThrust = false;
            _xRotateVal = 0;
            _curThrust = 0;
        });
     
    }

    private void GiveThrustOf(float val)
    {
        print("Applying Force of "+val);
        _curSpeed += 3f;
        _rigidbody.AddForce(transform.up * val,ForceMode.Force);
    }

    
    private void DisableThrust()
    {
        _hasReachedEnd = true;
        _applyDownwardThrust = false;
        _curSpeed = 0;
        _xRotateVal = 0;
        _floatingJoystick.gameObject.SetActive(false);
    } 
  
    
    #endregion


    #region Coroutines
    IEnumerator BoostSpeedCounter(float speed)
    {
        yield return new WaitForSeconds(timeForBoostedSpeed);
        _curSpeed = speed;
        _applyDownwardThrust = true;
        _isApplyingSwing = false;
    }

    IEnumerator NormalizeRotation(float time)
    {
        yield return new WaitForSeconds(time);
        _applyDownwardThrust = true;
        _xRotateVal = 0;
       
    }

    #endregion


    private void OnDestroy()
    {
        EventsManager.ONGameStart -= GiveInitialThrust;
        EventsManager.ONPassThroughHoop -= IncreaseSpeed;
        EventsManager.ONCollisionWithObstacle -= DecreaseSpeed;
        EventsManager.ONCollisionWithFan -= GiveFanUpwardThrust;
        EventsManager.ONGameWin -= DisableThrust;
        EventsManager.ONGameLose -= DisableThrust;
    }
}
