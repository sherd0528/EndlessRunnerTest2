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
      transform.Rotate(new Vector3(0f, 0f, 25f));
      if (target != null)
      {
         transform.Translate(vec * Time.deltaTime * 10f);
      }
   }

   public void Throw(Transform t)
   {
      vec = (t.position - transform.position).normalized;
      target = t;
      //transform.LookAt(target);
      //transform.localEulerAngles = new Vector3(90f, 0f, 0f);
   }
}
