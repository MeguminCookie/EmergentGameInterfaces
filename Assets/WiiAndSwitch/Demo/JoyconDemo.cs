using System;
using UnityEngine;
using static JoyconLib.Joycon;

namespace JoyconLib
{
    [Serializable]
    public struct ButtonGameObject
    {
        public Button button;
        public Transform transform;
    }

    public class JoyconDemo : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField] private bool leftHanded = true;

        [Header("Preview Variables")]
        [SerializeField] private Vector2 stick = Vector2.zero;
        [SerializeField] private Vector3 gyroscope = Vector3.zero;
        [SerializeField] private Vector3 acceleration = Vector3.zero;
        [SerializeField] private Quaternion orientation = Quaternion.identity;

        [Header("Visualisation")]
        [SerializeField] private ButtonGameObject[] buttonGameObjects;
        [SerializeField] private Transform stickIndicator;
        [SerializeField] private AcceleratorMode acceleratorMode;
        [SerializeField] private LineRenderer lineAcceleration;
        [SerializeField] private bool doOrientation = true;


        #endregion

        #region Local Variables

       public Joycon joycon;

        #endregion

        #region MonoBehaviour Functions

        private void OnEnable()
        {
            JoyconManager.OnJoyconInitialized += HandleJoyconInitialized;
            JoyconManager.OnJoyconDisposed += HandleJoyconDisposed;
        }

        private void OnDisable()
        {
            JoyconManager.OnJoyconInitialized -= HandleJoyconInitialized;
            JoyconManager.OnJoyconDisposed -= HandleJoyconDisposed;
        }

        // Update is called once per frame
        private void Update()
        {
            if (joycon != null)
            {
                stick = joycon.GetStick();
                stickIndicator.localPosition = new Vector3(stick.x, stick.y, 0);

                // Gyro values: x, y, z axis values (in radians per second)
                gyroscope = joycon.GetGyroscope();

                //Use Gyro to get a rotation
                orientation = joycon.GetOrientation();

                // Acceleration values:  x, y, z axis values (in Gs)
                switch (acceleratorMode)
                {
                    case AcceleratorMode.Local:
                        acceleration = joycon.GetAccelerationLocal();
                        break;
                    case AcceleratorMode.World:
                        acceleration = joycon.GetAccelerationWorld();
                        break;
                    case AcceleratorMode.WorldWithoutGravity:
                        acceleration = joycon.GetAccelerationWorldWithoutGravity();
                        break;
                }

                lineAcceleration.SetPositions(new Vector3[] {
                    lineAcceleration.transform.position,
                    lineAcceleration.transform.position + acceleration
                });

                if (doOrientation)
                    gameObject.transform.rotation = orientation;
            }
        }

        #endregion

        #region Event Handlers

        private void HandleJoyconInitialized(int index, Joycon joycon)
        {
            if (joycon != null && joycon.isLeft == leftHanded)
            {
                Debug.Log($"[JoyconDemo] Found a {(joycon.isLeft ? "left" : "right")} handed joycon. Subscribing to button events.");
                this.joycon = joycon;

                joycon.OnButtonPressed += HandleButtonPressed;
                joycon.OnButtonReleased += HandleButtonReleased;
            }
        }

        private void HandleJoyconDisposed(int index, Joycon joycon)
        {
            if (joycon != null && joycon.isLeft == leftHanded)
            {
                Debug.Log($"[JoyconDemo] A {(joycon.isLeft ? "left" : "right")} handed joycon is being disposed. Unsubscribing from button events.");

                joycon.OnButtonPressed -= HandleButtonPressed;
                joycon.OnButtonReleased -= HandleButtonReleased;
            }
        }

        private void HandleButtonPressed(bool leftController, Joycon.Button button)
        {
            //Debug.Log($"[Joycon] Pressed button {button} on the {(leftController ? "left" : "right")} controller.");

            //Simple way to highlight the pressed button
            bool found = false;
            int i = 0;

            while (!found && i < buttonGameObjects.Length)
            {
                if (buttonGameObjects[i].button == button)
                {
                    found = true;
                    buttonGameObjects[i].transform.Find("Background").GetComponent<SpriteRenderer>().color = Color.cyan;
                }

                i++;
            }

            //Joycons have no magnetometer, so they cannot accurately determine their yaw value.
            //Joycon.Recenter allows the user to reset the yaw value.
            //If the Home or Capture button is pressed, reset the orientation to fix orientation error.
            if (button == Joycon.Button.HOME || button == Joycon.Button.CAPTURE)
            {
                joycon.Recenter();
            }

            if (button == Joycon.Button.PLUS || button == Joycon.Button.MINUS)
            {
                // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
                // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

                joycon.SetRumble(160, 320, 0.6f, 200);

                // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
                // Then call SetRumble(0,0,0) when you want to turn it off.
            }
        }

        private void HandleButtonReleased(bool leftController, Joycon.Button button)
        {
            //Debug.Log($"[Joycon] Released button {button} on the {(leftController ? "left" : "right")} controller.");

            bool found = false;
            int i = 0;

            while (!found && i < buttonGameObjects.Length)
            {
                if (buttonGameObjects[i].button == button)
                {
                    found = true;
                    buttonGameObjects[i].transform.Find("Background").GetComponent<SpriteRenderer>().color = Color.white;
                }

                i++;
            }
        }

        #endregion
    }
}