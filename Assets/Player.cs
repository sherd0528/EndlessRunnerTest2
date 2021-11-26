using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Forever;

public class Player : MonoBehaviour
{
   public FloatingJoystick joystick;
   public Runner runner;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      runner.motion.offset += joystick.Direction * Time.deltaTime * 10;
   }
}
