using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMovement))]
[RequireComponent(typeof(Animator))]
public class AnimateGridMovement : MonoBehaviour {

    public float speed = 5f;
    Animator animator;
    SpriteRenderer spriteRenderer;
    int AnimatorHashDirectionX;
    int AnimatorHashDirectionY;
    int AnimateWalkTrigger;
    int AnimateDeathTrigger;

    List<Vector2Int> animateQueue;

    GridMovement gridPosition;
    bool isAnimate;

    public void TriggerWalk() {
        animator.SetTrigger(AnimateWalkTrigger);
    }

    public void DeathAnimation() {
        animator.SetBool(AnimateDeathTrigger, true);
    }

    void Awake() {
        gridPosition = gameObject.GetComponent<GridMovement>();
        isAnimate = false;
        animateQueue = new List<Vector2Int> { };

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //animator hashes
        animator = gameObject.GetComponent<Animator>();

        AnimatorHashDirectionX = Animator.StringToHash("DirectionX");
        AnimatorHashDirectionY = Animator.StringToHash("DirectionY");
        AnimateWalkTrigger = Animator.StringToHash("Walk");
        AnimateDeathTrigger = Animator.StringToHash("Death");
    }

    // Start is called before the first frame update
    void Start() {
        animator.SetBool(AnimateDeathTrigger, false);
        transform.position = gridPosition.ToWorldTransform();

    }

    public void AddToAnimateQueue(Vector2Int position) {
        animateQueue.Add(position);
    }

    // Update is called once per frame
    void LateUpdate() {
        spriteRenderer.sortingOrder = -1 * gridPosition.y;


        //update the animator params
        animator.SetFloat(AnimatorHashDirectionX, gridPosition.direction.x);
        animator.SetFloat(AnimatorHashDirectionY, gridPosition.direction.y);


        Vector3 desiredPosition = Vector3.zero;

        if (animateQueue.Count == 0) {
            isAnimate = false;
        }
        else {
            isAnimate = true;
            desiredPosition = gridPosition.ToWorldTransform(animateQueue[0]);
        }

        if (isAnimate) {
            Vector3 difference = new Vector3(desiredPosition.x - transform.position.x,
                desiredPosition.y - transform.position.y);

            if (difference.magnitude < 0.1f) {
                transform.position = desiredPosition;
                animateQueue.RemoveAt(0);
            }
            else {
                difference = Vector3.Normalize(difference);
                difference *= speed * Time.deltaTime;

                transform.position += difference;
            }


        }
    }

    public bool GetIsAnimate() { return isAnimate; }
}