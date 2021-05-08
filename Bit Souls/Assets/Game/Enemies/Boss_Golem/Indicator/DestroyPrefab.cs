using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{
    public Animator anim;
    public void Destroy()
	{
        Destroy(gameObject);
	}
}
