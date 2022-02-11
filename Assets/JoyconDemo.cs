﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour {
	
	private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public float speed = 10;

    void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}

		_renderer = GetComponent<Renderer>();
		_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];
			
			// GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
				Debug.Log ("Shoulder button 2 pressed");
				// GetStick returns a 2-element vector with x/y joystick components
				Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}",j.GetStick()[0],j.GetStick()[1]));
				
				j.SetRumble (120, 220, 0.3f, 100);

				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				j.Recenter();
			}
            
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp (Joycon.Button.SHOULDER_2))
			{
				Debug.Log ("Shoulder button 2 released");
			}
			
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton (Joycon.Button.SHOULDER_2))
			{
				Debug.Log ("Shoulder button 2 held");
			}

			if (j.GetButton(Joycon.Button.SHOULDER_1))
			{
				Debug.Log("Trigger sh 1");
				_renderer.material.color = Color.red;

			}
			
			if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("Rumble");

				// Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
				// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

				j.SetRumble (160, 320, 0.6f, 200);

				// The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
				// Then call SetRumble(0,0,0) when you want to turn it off.
				
			}

			if (j.GetButtonDown(Joycon.Button.SL))
			{
				j.SetRumble (120, 220, 0.3f, 100);

			}
			
			if (j.GetButtonDown(Joycon.Button.SR))
			{
				j.SetRumble (120, 220, 0.3f, 100);

			}

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

            orientation = j.GetVector();
            
            
			if (j.GetButton(Joycon.Button.DPAD_UP))
			{
				_renderer.material.color = Color.red;
			}
			else{
				_renderer.material.color = Color.blue;
			}
			
			switch (jc_ind)
			{
				case 0:
					FirstMove();
					break;
				case 1:
					SecondMove();
					break;
			}
        }

		void FirstMove()
		{
			_rigidbody.velocity = new Vector3(stick[1] * speed, _rigidbody.velocity.y,-stick[0] * speed);
		}
		
		void SecondMove()
		{
			_rigidbody.velocity = new Vector3(-stick[1] * speed, _rigidbody.velocity.y,stick[0] * speed);
		}
    }
}