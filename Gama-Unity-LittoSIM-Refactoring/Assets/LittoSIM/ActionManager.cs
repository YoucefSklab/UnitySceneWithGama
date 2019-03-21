using System;
using UnityEngine;

namespace ummisco.gama.unity.littosim
{
    public class ActionManager : MonoBehaviour
    {
        public Action action;

        public ActionManager(Action action)
        {
            this.action = action;
        }

        public void onAddButtonClicked()
        {
            LittosimManager.actionToDo = action.code;
        }
    }
}
