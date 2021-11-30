using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DmgText : MonoBehaviour
{
   TextMeshPro tmp;
   // Start is called before the first frame update
   void Start()
   {
      
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void Show(int dmg, Vector3 startPt)
   {
      tmp = GetComponent<TextMeshPro>();
      tmp.text = dmg.ToString();
      transform.position = startPt;
      transform.DOMoveY(startPt.y + 1.5f, 0.5f).OnComplete(() => { tmp.DOFade(0, 1f); });
   }
}
