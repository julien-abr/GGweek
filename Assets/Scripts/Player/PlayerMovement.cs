using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// J'ai tout mis dans ce script pour le moment, je m'occuperais de découper dans des scripts différents lorsque j'aurais établi les mécaniques générales :)
//
public class PlayerMovement : MonoBehaviour
{
    public FormManager formManager;


    public DashState dashState;
    private float jump = 7.75f;
    //private string playerMode = "default";
    private bool _canJump = true;
    private bool _canSendDashCDCoroutine = true;
    private bool _canSendDashingCoroutine = true;

    public Vector2 savedPos;
    public GameObject targetPos;


    SpriteRenderer sr;
    Rigidbody2D rb;

    private float _speedMultiplier = 11f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        formManager = new FormManager();
    }

    private void Update()
    {
        //Manage Dash execution
        switch (dashState)
        {
            case DashState.Ready:
                if (Input.GetKeyDown(KeyCode.F) && (formManager.formType == FormManager.FormType.Boar))
                {
                    savedPos = new Vector2(targetPos.transform.position.x , targetPos.transform.position.y);
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                {
                    rb.position = Vector2.Lerp(rb.position, savedPos, Time.deltaTime * 4);
                    if (_canSendDashingCoroutine)
                    {
                        StartCoroutine(Dashing());
                        
                        _canSendDashingCoroutine=false; 
                    }                  
                }
                break;
            case DashState.Cooldown:
                {
                    if (_canSendDashCDCoroutine)
                    {
                        StartCoroutine(DashCD());
                        _canSendDashCDCoroutine=false;
                    }
                }
                break;
        }

        //Manage Form Changing
        switch (formManager.formType)
        {
            case FormManager.FormType.Human:
                TurnToHuman();
                break;
            case FormManager.FormType.Boar:
                TurnToBoar();
                break;
            case FormManager.FormType.Bird:
                TurnToBird();
                break;
            case FormManager.FormType.Camel:
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            formManager.formType = FormManager.FormType.Boar;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            targetPos.SendMessage("ChangeSide", false);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            targetPos.SendMessage("ChangeSide", true);
        }

        float x = Input.GetAxis("Horizontal");
        x *= _speedMultiplier;
        if ((Input.GetAxis("Jump") > 0) && (_canJump))
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            _canJump = false;
        }
        rb.velocity = new Vector2(x, rb.velocity.y);
    }

    private void TurnToBird()
    {
        sr.color = Color.cyan;
        jump = 11f  ;
    }

    private void TurnToBoar()
    {
        sr.color = Color.black;
    }

    private void TurnToHuman()
    {
        sr.color = Color.white;
        jump = 7.75f;
    }

    private void TurnToCamel()
    {
        sr.color = Color.yellow;
    }

    IEnumerator DashCD()
    {
        yield return new WaitForSeconds(3);
        dashState = DashState.Ready;
        Debug.Log("now ready");
        _canSendDashCDCoroutine = true;
    }

    IEnumerator Dashing()
    {
        yield return new WaitForSeconds(0.5f);
        dashState = DashState.Cooldown;
        Debug.Log("now on cd");
        _canSendDashingCoroutine = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _canJump = true;
            _speedMultiplier = 11f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _canJump = false;
            _speedMultiplier = 7f;
        }
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}