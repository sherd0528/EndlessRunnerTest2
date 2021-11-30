using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mage : Player
{
   public static Mage Instance;
   // Start is called before the first frame update

   //public FixedJoystick joystick;
   public CharacterController cc;
   public Animator anim;

   //public SensorToolkit.Sensor sensor;
   public SensorToolkit.Sensor attackSensor;

   //public PlayMakerFSM fsm행동;
   public Transform castPosition;

   public AudioClip 발소리;
   public AudioClip 화염구_시전;

   Transform target = null;
   bool attackTarget = false;

   float attackDelay = 1;
   public float moveSpeed;

   bool hitReact = false;

   public ParticleSystem slashEffect;
   public ParticleSystem hitEffect;
   public AudioClip slashSound;

   int MaxHP = 10;
   public int HP = 10;

   public TextMeshProUGUI tmp;
   public ParticleSystem HPUpEffect;

   int requiredExp = 1;
   int exp = 0;
   public Renderer renderer;

   void Start()
   {
      DOTween.Init();
      Instance = this;      
   }

   // Update is called once per frame
   void Update()
   {
      //anim.SetFloat("MoveSpeed", joystick.Direction.magnitude);

      //return;

      if (joystick.Direction != Vector2.zero)
      {            
         if (hitReact == false)
         {
            runner.motion.offset += new Vector2(joystick.Horizontal * Time.deltaTime * moveSpeed, 0f);
            //Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.deltaTime;
            //cc.Move(moveDirection);

            //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0);
         }
      }
      
      if (attackSensor.GetNearest() != null)
      {
         //runner.followSpeed = 6f;
         if (hitReact == false && attackDelay >= 1f && attackSensor.GetNearest() != null)
         {
            
            attackDelay = 0;
         }
      }
      //else
      //   runner.followSpeed = 12f;

      attackDelay += Time.deltaTime;
   }

   //public void NoticedSensor(GameObject t, SensorToolkit.Sensor s)
   //{
   //   //target = t.transform;      
   //   //target = s.GetNearest().transform;
   //   print("detected");
   //}

   public void AttackSensor(GameObject t, SensorToolkit.Sensor s)
   {
      anim.Play("Attack");
      slashEffect.Play();
      var target = attackSensor.GetNearest();
      var enemy = target.GetComponent<Enemy>();
      if (enemy.Hit(1) == 0)
      {
         //exp++;
         //if (exp == requiredExp)
         //{
         //   requiredExp++;
         //   exp = 0;
         //   MaxHP = HP = MaxHP + 1;
         //   tmp.text = HP.ToString();
         //   HPUpEffect.Play();
         //}
      }

      var e = Instantiate(hitEffect);

      e.transform.position = target.transform.position + target.transform.forward;
      e.transform.position += target.transform.up;
      e.transform.localScale = Vector3.one * 3;
      e.Play();

      AudioSource.PlayClipAtPoint(slashSound, transform.position);

      Camera.main.DOShakePosition(0.1f, 0.25f);

      //runner.followSpeed = 6f;
   }

   public void AttackFinished()
   {
      //runner.followSpeed = attackSensor.GetNearest() == null ? 12f : 6f;
      runner.followSpeed = 12f;
   }

   public void Hit(int dmg)
   {
      this.HP = Mathf.Clamp(this.HP - dmg, 0, this.HP);
      tmp.text = "x" + HP.ToString() + "/" + MaxHP.ToString();

      renderer.material.color = Color.red;
      StartCoroutine(SC_NormalColor());
   }

   IEnumerator SC_NormalColor()
   {
      yield return new WaitForSeconds(0.1f);
      renderer.material.color = Color.white;
   }

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
