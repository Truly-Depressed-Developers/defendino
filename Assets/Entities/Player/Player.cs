using UnityEngine;

namespace Entities.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1f;
        void Update()     {
            if (InputManager.actions.Player.Left.IsPressed())
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationSpeed);
            } else if (InputManager.actions.Player.Right.IsPressed()) {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotationSpeed);
            }
        }
    }
}