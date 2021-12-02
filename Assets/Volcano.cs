using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
   // Start is called before the first frame update
   IEnumerator Start()
   {
      while(true)
      {
         for (int i=0; i<transform.childCount; i++)
         {
            var t = transform.GetChild(i);
            t.GetComponent<Shooter>().Shot(0);
            yield return new WaitForSeconds(0.5f);
         }

         yield return new WaitForSeconds(1f);
      }
   }

   // Update is called once per frame
   void Update()
   {

   }
}
