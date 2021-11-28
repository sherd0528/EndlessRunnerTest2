using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Forever;
using TMPro;

public class SegementBulder : Builder
{
   // Start is called before the first frame update
   static int build_count = 0;
   public Transform enemies;

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

      print(build_count++);

      foreach (Transform e in enemies)
      {
         if (e.gameObject.activeSelf)
         {
            e.GetComponent<Enemy>().SetHP(Random.Range((int)Mathf.Max(1, Mage.Instance.HP / 2f), (int)(Mage.Instance.HP * 2.5f)));
         }
      }
   }
}
