using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button3D : MonoBehaviour
{
    public GameObject menuToOpen;
    private void OnMouseDown() { menuToOpen.active = !menuToOpen.active;  }
}
