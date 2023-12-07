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
        // get main camera
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // get logic manager
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

            if (_holder.MousePos.x < _holder.transform.position.x)
            {
                // el jugador esta apuntando a la izquierda del munheco
                this.transform.position = new Vector3(_holder.transform.position.x - HandOffset.x,
                                                      _holder.transform.position.y - HandOffset.y,
                                                      0);
            }
            else
            {
                // el jugador esta apuntando a la derecha del munheco
                this.transform.position = new Vector3(_holder.transform.position.x + HandOffset.x,
                                                      _holder.transform.position.y + HandOffset.y,
                                                      0);
            }
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
