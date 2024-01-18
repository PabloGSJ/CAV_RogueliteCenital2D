using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorMainMenuScript : MonoBehaviour
{

    public Texture2D cursorSword;
    void Start()
    {
        Cursor.SetCursor(cursorSword, Vector2.zero, CursorMode.ForceSoftware);
    }
}
