using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Soldier : Deployable
{

    public int soldierDPS;


    private void Update()
    {
        //work on unit select detection. discontunied
        if (Input.GetMouseButton(0))
        {
            Transform building;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null)
            {
                if (hit.collider == this.gameObject.GetComponent<Collider2D>())
                {

                }
            }
        }
    }
}
