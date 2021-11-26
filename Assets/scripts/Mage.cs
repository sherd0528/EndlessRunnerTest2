using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
   // Start is called before the first frame update

   public FixedJoystick joystick;
   public CharacterController cc;
   public Animator anim;

   //public SensorToolkit.Sensor sensor;
   //public SensorToolkit.Sensor attackSensor;

   //public PlayMakerFSM fsm행동;
   public Transform castPosition;

   public AudioClip 발소리;
   public AudioClip 화염구_시전;

   Transform target = null;
   bool attackTarget = false;

   float attackDelay = 1;
   float moveSpeed = 1;

   bool hitReact = false;

   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      anim.SetFloat("MoveSpeed", joystick.Direction.magnitude);

      if (joystick.Direction != Vector2.zero)
      {            
         if (hitReact == false)
         {
            Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.deltaTime;
            cc.Move(moveDirection);

            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0);
         }
      }
      //else if (sensor.GetNearest() != null)
      //{  
      //   if (hitReact == false && attackDelay >= 3 && attackSensor.GetNearest() != null)
      //   {
      //      target = attackSensor.GetNearest().transform;
      //      anim.Play("Attack1");
      //      print("attack");
      //      attackDelay = 0;
      //   }
         
      //   NS2HUtil.LookAtSmooth.Look(transform, sensor.GetNearest().transform.position, 5);
      //}

      attackDelay += Time.deltaTime;
   }

   //public void NoticedSensor(GameObject t, SensorToolkit.Sensor s)
   //{
   //   //target = t.transform;      
   //   //target = s.GetNearest().transform;
   //   print("detected");
   //}

   //public void AttackSensor(GameObject t, SensorToolkit.Sensor s)
   //{
   //   attackTarget = true;
   //}

   //public void AttackSensorLost(GameObject t, SensorToolkit.Sensor s)
   //{
   //   attackTarget = false;
   //}

   public void Hit()
   {
      if (joystick.Direction != Vector2.zero)
      {
         hitReact = true;
         anim.Play("StandReact");
         print("c1");
      }
      else if (Random.Range(0f, 1f) < 0.5f)
      {
         hitReact = true;         
         anim.Play("StandReact");
         print("c2");
      }
   }

   #region 애니메이션 이벤트
   public void CastMagic()
   {
      if (target == null) return;

      GameObject prefab = Resources.Load("Magics/Fireball") as GameObject;
      GameObject go = Instantiate(prefab) as GameObject;

      go.transform.position = castPosition.position;      
      go.transform.LookAt(target.transform.position + Vector3.up * 0.5f);
   }

   public void FootStep()
   {
      AudioSource.PlayClipAtPoint(발소리, transform.position);
   }

   public void EndReact()
   {
      hitReact = false;
   }
   #endregion
}
