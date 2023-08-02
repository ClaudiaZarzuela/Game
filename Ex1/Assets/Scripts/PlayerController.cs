using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset walking;
    public AnimationReferenceAsset jumping;
    public float speed;
    public float movement;
    public float jumpSpeed;
    public Rigidbody2D rigidbody;
    public string currentState;
    public string previousState;
    public string currentAnimation;
    // Start is called before the first frame update
    void Start()
    {
        currentState = "Idle";
        setCharacterState(currentState);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();   
    }
    public void setAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation)) return;
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }
    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("Jumping"))
        {
            setCharacterState(previousState);
        }
    }
    public void setCharacterState(string state)
    {
        if (state.Equals("Walking")) setAnimation(walking, true, 2.0f);
        else if (state.Equals("Jumping")) setAnimation(jumping, false, 1.0f);
        else setAnimation(idle, true, 1.0f);

        currentState = state;
    }
    public void move()
    {
        movement = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);
        if (movement != 0)
        {
            if (!currentState.Equals("Jumping")) setCharacterState("Walking");
            if (movement > 0) transform.localScale = new Vector2(1f, 1f);
            else transform.localScale = new Vector2(-1f, 1f);
        }
        else { if (!currentState.Equals("Jumping")) setCharacterState("Idle"); }
        if (Input.GetButtonDown("Jump")) jump();
    }
    public void jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        if (!currentState.Equals("Jumping")) previousState = currentState;
        setCharacterState("Jumping");
    }
}
