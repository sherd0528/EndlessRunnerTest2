using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
         transform.Translate(vec * Time.deltaTime * 30f);
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
}
