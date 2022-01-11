using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb; 
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    public Weapon weapon;

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction fire;
    private InputAction aim;
    private InputAction back;

    private void Awake(){
         playerControls = new PlayerInputActions();
    }

    private void OnEnable(){
        move = playerControls.Player.Move;
        move.Enable();

        aim = playerControls.Player.Aim;
        aim.Enable();
        
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        back = playerControls.Player.Back;
        back.Enable();
        back.performed += BackToMenu;
    }

    private void OnDisable(){
        move.Disable();
        aim.Disable();
        fire.Disable();
        back.Disable();
    }
 
    void Start(){
    }

    void Update(){
        ProcesInputs();   
    }

    void FixedUpdate(){
        Move();
    }

    void ProcesInputs(){
        moveDirection = move.ReadValue<Vector2>().normalized; 
        mousePosition = aim.ReadValue<Vector2>();
        mousePosition = (Vector2) sceneCamera.ScreenToWorldPoint(mousePosition); 
    }

    void Fire(InputAction.CallbackContext context){
        // Debug.Log("we fired");
        // weapon.Fire(); // old not pooled bullet spawning
        GameObject obj = ObjectPooler.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = weapon.firePoint.position;
        obj.transform.rotation = weapon.firePoint.rotation;  
        obj.SetActive(true);
    }

    void Move(){
        // move player  
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        
        // rotate player
        Vector2 aimDirection = mousePosition - rb.position;

        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg -90f;
        rb.rotation = aimAngle;
    }

    void BackToMenu(InputAction.CallbackContext context){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

}
 