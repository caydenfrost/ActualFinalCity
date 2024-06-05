using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ReturnHome : MonoBehaviour
    {
        [SerializeField] private List<GameObject> selectedObjs;
        private bool clear;
        void LateUpdate()
        {
            if (clear)
            {
                selectedObjs.Clear();
                clear = false;
            }
        }
        public void CallReturnHome()
        {
            if (selectedObjs.Count > 0)
            {
                foreach (GameObject selectedObj in selectedObjs)
                {
                    NavAndAICore navigation = selectedObj.GetComponent<NavAndAICore>();
                    if (navigation != null)
                    {
                        clear = true;
                    }
                }
            }
            selectedObjs.Clear();
        }
        
    }