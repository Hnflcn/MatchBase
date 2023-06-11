using Lean.Touch;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private bool _isFull;
        private bool _canMove;
    
        private float upBorder;
        private float downBorder;
        private float rightBorder;
        private float leftBorder;

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
            LeanTouch.OnFingerUpdate += OnFingerUpdate;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerUp -= OnFingerUp;
            LeanTouch.OnFingerUpdate -= OnFingerUpdate;
        }

        private void OnFingerDown(LeanFinger finger)
        {
            _canMove = true;
        
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.collider.transform.TryGetComponent(out Objects objects))
            {
                
            }

        }

        private void OnFingerUp(LeanFinger finger)
        {
            _isFull = false;
            _canMove = false;
        }


        private static void OnFingerUpdate(LeanFinger finger)
        {
        
        }

    }
}

