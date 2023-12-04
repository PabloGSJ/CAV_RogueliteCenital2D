using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour 
{
    // CONTEXT:

    public Camera cam;
    public Rigidbody2D rb;
    public CircleCollider2D trig;
    protected DisplayManager ui;
    public SpriteRenderer sr;

    // Holding variables
    protected PlayerStateMachine _holder = null;
    public Vector2 HandOffset;
    protected Vector2 _shootingVector;

    // Shoot variables
    public float Cadence = 0f;  // TODO

    // MONO BEHAVIOUR FUNCTION:

    // at the begining, before Start
    private void Awake()
    {
        // setup logic manager
        ui = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<DisplayManager>();
    }

    // update for interactions involving physics engine
    void FixedUpdate()
    { 
        // follow mouse pointer while on hand
        if (_holder != null)
        {
            // rotate the weapon
            _shootingVector = _holder.MousePos - rb.position;
            rb.MoveRotation(Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg);
        }
    }


    // AUXILIARY FUNCTIONS:

    // Shoot a bullet
    public abstract void Shoot(float dmgMod);

    // Each child manages display independently
    protected abstract void DisplayUp();
    protected abstract void DisplayDown();

    // Sets a new holder for the weapon
    public void Pickedup(PlayerStateMachine player)
    {
        trig.enabled = false;
        this._holder = player;
        this.transform.parent = player.transform;
        this.transform.position = new Vector3(player.transform.position.x + HandOffset.x, 
                                              player.transform.position.y + HandOffset.y, 
                                              0);
        this.sr.sortingOrder = 1;

        DisplayUp();
    }

    // Unsets the weapon holder
    public IEnumerator Dropped()
    {
        DisplayDown();

        this._holder = null;
        this.transform.parent = null;
        this.sr.sortingOrder = -1;
        yield return new WaitForSeconds(1f);    // wait for the button unpress
        trig.enabled = true;
    }
}
