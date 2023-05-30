using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLogic : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] public KeyCode _JumpKey = KeyCode.Space;
    [SerializeField] public KeyCode _slideKey = KeyCode.LeftShift;
    // fight key right click
    [SerializeField] public KeyCode _fightKey = KeyCode.Mouse1;

    //Shoot key left click
    [SerializeField] public KeyCode _shootKey = KeyCode.Mouse0;

    private Vector2 _axis = Vector2.zero;

    public AudioClip _jumpSound;
    public AudioClip _slideSound;
    public AudioClip _fightSound;
    

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    private void HeroLogicUpate()
    {

        _axis.x  = Input.GetAxis("Horizontal");
        _axis.y  = Input.GetAxis("Vertical");

        if (MoveScript.instance.move == false)
        {
            return;
        }
        if(_anim != null)
        {
            if(Input.GetKeyDown(_JumpKey) && MoveScript.instance.isJumping == false)
            {
                MoveScript.instance.isJumping = true;
                MoveScript.instance.Jump();
                _anim.SetTrigger("Jump");        
                AudioSource.PlayClipAtPoint(_jumpSound, transform.position, 0.2f);
            }
        


            if(Input.GetKeyDown(_slideKey))
            {
                _anim.SetTrigger("Slide");
                AudioSource.PlayClipAtPoint(_slideSound, transform.position, 0.2f);
            }

            // if fight key is pressed then fight till the key is pressed
            if(Input.GetKeyDown(_fightKey))
            {
                _anim.SetFloat("Fight", 1);
                AudioSource.PlayClipAtPoint(_fightSound, transform.position, 0.2f);

            }
            if(Input.GetKeyUp(_fightKey))
            {
                _anim.SetFloat("Fight", 0);
            }   

            // if shoot key is pressed then shoot till the key is pressed
            if(Input.GetKeyDown(_shootKey))
            {
                _anim.SetFloat("Shoot", 1);
            }
            if(Input.GetKeyUp(_shootKey))
            {
                _anim.SetFloat("Shoot", 0);
            }


        }

        _anim.SetFloat("v", _axis.y);
        _anim.SetFloat("h", _axis.x);

       
    }

    





    // Update is called once per frame
    void Update()
    {
        HeroLogicUpate();
    }



}
