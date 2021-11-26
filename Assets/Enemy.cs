using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   public Animator anim;
   public NavMeshAgent nma;
   Transform target;

   // Start is called before the first frame update
   void Start()
   {
      
   }

   // Update is called once per frame
   void Update()
   {
      if (nma.enabled == false) return;
      if (target == null) return;

      nma.destination = target.position;
      anim.SetFloat("Distance", nma.remainingDistance);
      transform.LookAt(target);
   }

   private void OnParticleCollision(GameObject other)
   {
      anim.Play("GetHit");
      nma.enabled = false;
   }

   void RecoverFromHit()
   {
      nma.enabled = true;
      if (target != null)
      {
         anim.Play("WalkFWD");
         print("check");
      }
      else
      {
         anim.Play("IdleBattle");
      }
   }

   //public void NoticeSensorDetected(GameObject t, SensorToolkit.Sensor s)
   //{
   //   target = t.transform;      
   //   anim.Play("WalkFWD");
   //   print(t.name);
   //}

   //public void NoticeSensorLostDetection(GameObject t, SensorToolkit.Sensor s)
   //{
   //   target = null;
   //   nma.destination = transform.position;
   //   anim.Play("IdleBattle");
   //}

   public void MotionAttack()
   {
      target.GetComponent<Mage>().Hit();

      print(nma.remainingDistance);
      
   }
}
