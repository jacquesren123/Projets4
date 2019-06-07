using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Test
{
    public class ChooseClass : MonoBehaviour
    {
        public static bool Prop = true;

        public void OnClickConnectToRoomHunter()
        {

            Prop = false;

        }

        public void OnClickConnectToRoomProp()
        {

            Prop = true;

        }

        public bool GetProp()
        {
            Debug.Log(Prop);
            return Prop;
        }
    }
}
