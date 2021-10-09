using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Guardian.Modules.SkillHelpers
{
    public class ShieldBubble : MonoBehaviour
    {
        private int state;
        private float shieldRadius;

        internal static GameObject CreateShieldBubble()
        {
            GameObject shieldBubble = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            shieldBubble.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            shieldBubble.gameObject.layer = 14;

            shieldBubble.AddComponent<ShieldBubble>();

            // Create colliders

            shieldBubble.GetComponent<ShieldBubble>().SetState(0);

            return shieldBubble;
        }

        public void SetState(int newState)
        {
            state = newState;
        }

        public void SetupVariables(float radius)
        {
            shieldRadius = radius;
        }

        private void FixedUpdate()
        {
            switch(state)
            {
                case 0:
                    Grow(shieldRadius);
                    break;

                case 1:
                    // exist
                    break;

                case 2:
                    ShrinkAndDestroy();
                    break;

                default:
                    break;
            }
        }

        private void Grow(float shieldRadius)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, shieldRadius, 0.1f), Mathf.Lerp(transform.localScale.y, shieldRadius, 0.1f), Mathf.Lerp(transform.localScale.z, shieldRadius, 0.1f));
            // Grow collider
            // Push enemies

            if (state != 1 && transform.localScale.x >= shieldRadius - 0.05f)
            {
                SetState(1);
            }
        }

        private void ShrinkAndDestroy()
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0.01f, 0.1f), Mathf.Lerp(transform.localScale.y, 0.01f, 0.1f), Mathf.Lerp(transform.localScale.z, 0.01f, 0.1f));
            // Shrink collider
            Destroy(gameObject, 2f);
        }
    }
}
