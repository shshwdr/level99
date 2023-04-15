using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MouseController : MonoBehaviour
{
    public LayerMask clickLayerMask; // The layers to check for clickable items

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D[] hitColliders = Physics2D.OverlapPointAll(clickPos);

            if (hitColliders.Length > 0)
            {
                Collider2D closestCollider = null;
                float closestDistance = float.MaxValue;

                foreach (Collider2D collider in hitColliders)
                {
                    float distance = Vector2.Distance(clickPos, collider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = collider;
                    }
                }

                if (closestCollider != null)
                {
                    PlayerSkillManager.Instance.mouseClick(closestCollider.GetComponent<Character>());
                    return;
                    // Do something with the closest clicked collider
                }
            }
            PlayerSkillManager.Instance.mouseClick(null);
        }
    }
}

