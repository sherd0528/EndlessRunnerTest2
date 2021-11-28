using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Forever;

public class BossSegment : Builder
{
   public Runner boss;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   protected override void Build()
   {
      base.Build();
      //boss.followSpeed = 12f;
      boss.startMode = Runner.StartMode.Percent;

      boss.transform.SetParent(null);

      StartCoroutine(SC_MoveUp());

      print("Entered boss segment");
   }

   IEnumerator SC_MoveUp()
   {
      float dur = 2f;
      float d = 0f;
      float p = 0f;
      while (d < 1f)
      {
         p = Mathf.Lerp(0.5f, 5f, d);
         d += Time.deltaTime / dur;

         boss.motion.offset = new Vector3(0f, p);
         yield return null;
      }

      d = 0f;
      while (d < 1f)
      {
         p = Mathf.Lerp(0f, 12f, d);
         d += Time.deltaTime / dur;

         boss.followSpeed = p;
         yield return null;
      }

      boss.followSpeed = 12f;
   }
}
