using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGM : MonoBehaviour
{
    public GameObject myTextPanel;
    protected bool _active = false;

    public abstract void UseGM(PlayerStateMachine player);
}
