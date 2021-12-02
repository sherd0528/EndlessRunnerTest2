using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shooter : MonoBehaviour
{
   public ParticleSystem bullet;
   public Transform target;
   public float firingAngle = 50.0f;
   public float gravity = 9.8f;

   // Start is called before the first frame update
   void Start()
   {
      
   }

   private void Awake()
   {
      target.gameObject.SetActive(false);
   }

   // Update is called once per frame
   void Update()
   {

   }

   IEnumerator SimulateProjectile()
   {
      // Short delay added before Projectile is thrown
      //yield return new WaitForSeconds(1.5f);
      firingAngle = 75f;
      bullet.transform.position = transform.position + Vector3.up * 1.5f;
      // Move projectile to the position of throwing object + add some offset if needed.
      bullet.transform.position = transform.position + new Vector3(0, 0.0f, 0);

      // Calculate distance to target
      float target_Distance = Vector3.Distance(bullet.transform.position, target.position);

      // Calculate the velocity needed to throw the object to the target at specified angle.
      float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

      // Extract the X  Y componenent of the velocity
      float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
      float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

      // Calculate flight time.
      float flightDuration = target_Distance / Vx;

      // Rotate projectile to face the target.
      bullet.transform.rotation = Quaternion.LookRotation(target.position - bullet.transform.position);

      float elapse_time = 0;

      while (elapse_time < flightDuration)
      {
         bullet.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

         elapse_time += Time.deltaTime;

         yield return null;
      }

      //print("Done");
      target.gameObject.SetActive(false);
   }

   IEnumerator SC_Shot()
   {
      while (true)
      {
         //bullet.transform.LookAt(target.position);
         //bullet.transform.localEulerAngles = new Vector3(-65f, bullet.transform.localEulerAngles.y, 0f);
         ////var main = bullet.main;
         ////main.startSpeed = Vector3.Distance(bullet.transform.position, target.transform.position);
         //bullet.Play();
         bullet.Play();
         yield return StartCoroutine(SimulateProjectile());
         yield return new WaitForSeconds(4f);
      }
   }

   IEnumerator SC_TargetSize(float size)
   {
      float dur = 1.5f;
      float d = 0f;

      target.gameObject.SetActive(true);

      while (d < 1f)
      {
         var v = Mathf.Lerp(0.1f, size, d);
         d += Time.deltaTime / dur;

         target.GetComponent<Projector>().orthographicSize = v;

         yield return null;
      }
   }

   public void Shot(float height)
   {
      bullet.transform.position = transform.parent.position;
      bullet.Play();
      StartCoroutine(SC_TargetSize(3));
      StartCoroutine(SimulateProjectile());

      return;

      var path = new Vector3[6];

      var vec_center = Vector3.Lerp(transform.position, target.position, 0.5f);
      path[0] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      path[1] = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
      vec_center = Vector3.Lerp(transform.position, target.position, 0.25f);
      path[2] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      path[3] = target.position;
      vec_center = Vector3.Lerp(transform.position, target.position, 0.75f);
      path[4] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      path[5] = target.position + target.up;

      //path[0] = transform.position;
      //var vec_center = Vector3.Lerp(transform.position, target.position, 0.25f);
      //path[1] = new Vector3(vec_center.x, vec_center.y + height / 2f, vec_center.z);
      //vec_center = Vector3.Lerp(transform.position, target.position, 0.5f);
      //path[2] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      //vec_center = Vector3.Lerp(transform.position, target.position, 0.9f);
      //path[3] = new Vector3(vec_center.x, vec_center.y + height/2f, vec_center.z);
      //path[4] = target.position;

      bullet.transform.DOPath(path, 3f, PathType.CubicBezier);
   }
}
