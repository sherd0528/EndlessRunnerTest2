using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
   public Animator anim;
   [HideInInspector]
   public int MaxHP;
   int hp;
   public TextMeshPro tmp;

   Runner runner;

   public ParticleSystem hitEffect;

   // Start is called before the first frame update
   void Start()
   {
      //anim = GetComponent<Animator>();
      //tm = GetComponentInChildren<TextMesh>();
      runner = GetComponent<Runner>();

      //MaxHP = this.hp = 1;
   }

   // Update is called once per frame
   void Update()
   {
      
   }

   public void SetHP(int v)
   {
      hp = v;
      MaxHP = v;
      tmp.text = hp.ToString();
   }

   private void OnParticleCollision(GameObject other)
   {
   
   }

   public int Hit(int dmg)
   {
      this.hp = Mathf.Clamp(this.hp - dmg, 0, this.hp);
      tmp.text = hp.ToString();

      GameObject prefab = Resources.Load("DmgText") as GameObject;
      GameObject go = Instantiate(prefab) as GameObject;
      go.transform.GetComponent<DmgText>().Show(-1, transform.position + Vector3.up);

      print("Monster HP: " + this.hp);      

      if (hp > 0)
      {
         StartCoroutine(SC_Back());
      }
      else
         anim.Play("Die");

      //anim.Play("Die");
      return hp;      
   }

   public void AttackFinished()
   {
      print("AttackFinished");

      

      //if (this.hp == 0)
      //{
      //   anim.Play("Die");
      //}
      //else
      //{
      //   runner.followSpeed = 60f;         
      //}

   }

   IEnumerator SC_Back()
   {
      anim.Play("Attack02");
      hitEffect.Play();
      Mage.Instance.Hit(1);

      runner.followSpeed = 30f;      
      yield return new WaitForSeconds(0.12f);
      runner.followSpeed = 0.5f;
   }

   void RecoverFromHit()
   {
      
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
      
   }
}
