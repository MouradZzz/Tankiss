using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoyconInputHandler : MonoBehaviour {
	
	private List<Joycon> joycons;
	
	[HideInInspector]
	public float[] stick;
	public int jc_ind = 0;

    #region Unity Action Call

    public UnityAction OnClickSlEvent;
    public UnityAction OnClickDpadDownEvent;
    public UnityAction OnClickDpadUpEvent;

    #endregion


    #region Unity CallBack Function
    void Start ()
    {
        joycons = JoyconManager.Instance.j;
        
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}
    }

    void Update () 
    {
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];
			
			if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			{
				OnClickDpadDownEvent.Invoke();
			}
			
			if (j.GetButtonDown(Joycon.Button.DPAD_UP))
			{
				OnClickDpadUpEvent.Invoke();
			}

			if (j.GetButtonDown(Joycon.Button.SL))
			{
				j.SetRumble (120, 220, 0.3f, 100);

				OnClickSlEvent.Invoke();
			}
			
            stick = j.GetStick();
            
        }
		
    }
    #endregion

    
}