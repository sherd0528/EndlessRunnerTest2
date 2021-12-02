using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shuriken : MonoBehaviour
{
   Transform target = null;
   Vector3 vec;
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {      
      if (target != null)
      {
         transform.Translate(vec * Time.deltaTime * 20f);
      }
   }

   public void Throw(Transform t)
   {
      vec = (t.position - transform.position).normalized;
      vec = new Vector3(vec.x, 0f, vec.z);
      target = t;
      //transform.LookAt(target);
      //transform.localEulerAngles = new Vector3(90f, 0f, 0f);
   }

   private void OnCollisionEnter(Collision collision)
   {
      GameObject prefab = Resources.Load("ShurikenHitEffect") as GameObject;
      GameObject go = Instantiate(prefab) as GameObject;
      go.transform.position = transform.position;
      
      var e = collision.gameObject.GetComponent<Enemy>();
      if (e == null) return;
      if (e.hp <= 0) return;

      Camera.main.DOShakePosition(0.1f, 0.25f);
      if (e) e.Hit(1);
      else print("no e");

      Destroy(gameObject);
   }
}
