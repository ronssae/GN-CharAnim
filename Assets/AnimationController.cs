using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = anim.GetBool("isWalking");
        bool forwardPressed = Input.GetKeyDown(KeyCode.W);

        if (!isWalking && forwardPressed) anim.SetBool("isWalking", true);
        if (isWalking && !forwardPressed) anim.SetBool("isWalking", false);
    }
}
