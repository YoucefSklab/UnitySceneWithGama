using UnityEngine;
using System.Collections;


namespace ummisco.gama.unity.littosim
{
    public class UIManager : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void setPnelActive(string panelName)
        {
            Debug.Log("Panel pressed  -------------> "+ panelName);

            if (panelName.Equals(IUILittoSim.ONGLET_AMENAGEMENT))
            {
                GameObject.Find(IUILittoSim.DEFENSE_PANEL).SetActive(false);
                GameObject.Find(IUILittoSim.MAP_PANEL).SetActive(true);
            }
            else if (panelName.Equals(IUILittoSim.ONGLET_DEFENSE))
            {
                GameObject.Find(IUILittoSim.MAP_PANEL).SetActive(false);
                GameObject.Find(IUILittoSim.DEFENSE_PANEL).SetActive(true);
            }
        }
    }

}