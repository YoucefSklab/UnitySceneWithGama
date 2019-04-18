using System;
using System.Collections.Generic;
using UnityEngine;

namespace ummisco.gama.unity.littosim
{
    public class ILangue
    {
        public static Dictionary<string, string> current_langue = new Dictionary<string, string>();

        public static string ACTION_REPAIR_DIKE = "ACTION_REPAIR_DIKE";
        public static string ACTION_CREATE_DIKE = "ACTION_CREATE_DIKE";
        public static string ACTION_DESTROY_DIKE = "ACTION_DESTROY_DIKE";
        public static string ACTION_RAISE_DIKE = "ACTION_RAISE_DIKE";
        public static string ACTION_INSTALL_GANIVELLE = "ACTION_INSTALL_GANIVELLE";
        public static string ACTION_MODIFY_LAND_COVER_AU = "ACTION_MODIFY_LAND_COVER_AU";
        public static string ACTION_MODIFY_LAND_COVER_A = "ACTION_MODIFY_LAND_COVER_A";
        public static string ACTION_MODIFY_LAND_COVER_U = "ACTION_MODIFY_LAND_COVER_U";
        public static string ACTION_MODIFY_LAND_COVER_N = "ACTION_MODIFY_LAND_COVER_N";
        public static string ACTON_MODIFY_LAND_COVER_FROM_AU_TO_N = "ACTON_MODIFY_LAND_COVER_FROM_AU_TO_N";
        public static string ACTON_MODIFY_LAND_COVER_FROM_A_TO_N = "ACTON_MODIFY_LAND_COVER_FROM_A_TO_N";
        public static string ACTION_MODIFY_LAND_COVER_AUs = "ACTION_MODIFY_LAND_COVER_AUs";
        public static string ACTION_MODIFY_LAND_COVER_Us = "ACTION_MODIFY_LAND_COVER_Us";
        public static string ACTION_MODIFY_LAND_COVER_Ui = "ACTION_MODIFY_LAND_COVER_Ui";
        public static string ACTION_EXPROPRIATION = "ACTION_EXPROPRIATION";
        public static string ACTION_MODIFY_LAND_COVER_AUs_SUBSIDY = "ACTION_MODIFY_LAND_COVER_AUs_SUBSIDY";
        public static string ACTION_MODIFY_LAND_COVER_Us_SUBSIDY = "ACTION_MODIFY_LAND_COVER_Us_SUBSIDY";
        public static string ACTION_INSPECT_LAND_USE = "ACTION_INSPECT_LAND_USE";
        public static string ACTION_INSPECT_DIKE = "ACTION_INSPECT_DIKE";
        public static string ACTION_DISPLAY_PROTECTED_AREA = "ACTION_DISPLAY_PROTECTED_AREA";
        public static string ACTION_DISPLAY_FLOODED_AREA = "ACTION_DISPLAY_FLOODED_AREA";
        public static string MSG_NEW_ROUND = "MSG_NEW_ROUND";
        public static string MSG_GAME_DONE = "MSG_GAME_DONE";
        public static string MSG_LOG_USER_ACTION = "MSG_LOG_USER_ACTION";
        public static string MSG_CONNECT_ACTIVMQ = "MSG_CONNECT_ACTIVMQ";
        public static string MSG_SIM_NOT_STARTED = "MSG_SIM_NOT_STARTED";
        public static string MSG_NO_FLOOD_FILE_EVENT = "MSG_NO_FLOOD_FILE_EVENT";
        public static string MSG_OK_CONTINUE = "MSG_OK_CONTINUE";
        public static string MSG_SUBMERSION_NUMBER = "MSG_SUBMERSION_NUMBER";
        public static string MSG_NUMBER = "MSG_NUMBER";
        public static string HELP_MSG_REPAIR_DIKE = "HELP_MSG_REPAIR_DIKE";
        public static string HELP_MSG_CREATE_DIKE = "HELP_MSG_CREATE_DIKE";
        public static string HELP_MSG_DESTROY_DIKE = "HELP_MSG_DESTROY_DIKE";
        public static string HELP_MSG_RAISE_DIKE = "HELP_MSG_RAISE_DIKE";
        public static string HELP_MSG_INSTALL_GANIVELLE = "HELP_MSG_INSTALL_GANIVELLE";
        public static string HELP_MSG_MODIFY_LAND_COVER_AU = "HELP_MSG_MODIFY_LAND_COVER_AU";
        public static string HELP_MSG_MODIFY_LAND_COVER_A = "HELP_MSG_MODIFY_LAND_COVER_A";
        public static string HELP_MSG_MODIFY_LAND_COVER_U = "HELP_MSG_MODIFY_LAND_COVER_U";
        public static string HELP_MSG_MODIFY_LAND_COVER_N = "HELP_MSG_MODIFY_LAND_COVER_N";
        public static string HELP_MSG_MODIFY_LAND_COVER_FROM_AU_TO_N = "HELP_MSG_MODIFY_LAND_COVER_FROM_AU_TO_N";
        public static string HELP_MSG_MODIFY_LAND_COVER_FROM_A_TO_N = "HELP_MSG_MODIFY_LAND_COVER_FROM_A_TO_N";
        public static string HELP_MSG_MODIFY_LAND_COVER_AUs = "HELP_MSG_MODIFY_LAND_COVER_AUs";
        public static string HELP_MSG_MODIFY_LAND_COVER_Us = "HELP_MSG_MODIFY_LAND_COVER_Us";
        public static string HELP_MSG_MODIFY_LAND_COVER_Ui = "HELP_MSG_MODIFY_LAND_COVER_Ui";
        public static string HELP_MSG_EXPROPRIATION = "HELP_MSG_EXPROPRIATION";
        public static string HELP_MSG_MODIFY_LAND_COVER_AUs_SUBSIDY = "HELP_MSG_MODIFY_LAND_COVER_AUs_SUBSIDY";
        public static string HELP_MSG_MODIFY_LAND_COVER_Us_SUBSIDY = "HELP_MSG_MODIFY_LAND_COVER_Us_SUBSIDY";
        public static string HELP_MSG_INSPECT_LAND_USE = "HELP_MSG_INSPECT_LAND_USE";
        public static string HELP_MSG_INSPECT_DIKE = "HELP_MSG_INSPECT_DIKE";
        public static string LDR_MSG_SEND_MONEY = "LDR_MSG_SEND_MONEY";
        public static string LDR_MSG_WITHDRAW_MONEY = "LDR_MSG_WITHDRAW_MONEY";
        public static string LDR_MSG_SEND_MONEY_TO = "LDR_MSG_SEND_MONEY_TO";
        public static string LDR_MSG_TAKE_MONEY_FROM = "LDR_MSG_TAKE_MONEY_FROM";
        public static string LDR_MSG_SEND_MSG = "LDR_MSG_SEND_MSG";
        public static string LDR_MSG_SEND_MSG_TO = "LDR_MSG_SEND_MSG_TO";
        public static string LDR_MSG_AMOUNT_REVENUE = "LDR_MSG_AMOUNT_REVENUE";
        public static string LDR_MSG_AMOUNT_SUBSIDY = "LDR_MSG_AMOUNT_SUBSIDY";
        public static string BTN_TAKE_MONEY_MSG1 = "BTN_TAKE_MONEY_MSG1";
        public static string BTN_TAKE_MONEY_MSG2 = "BTN_TAKE_MONEY_MSG2";
        public static string BTN_TAKE_MONEY_MSG3 = "BTN_TAKE_MONEY_MSG3";
        public static string BTN_TAKE_MONEY_MSG4 = "BTN_TAKE_MONEY_MSG4";
        public static string MSG_CHOOSE_MSG_TO_SEND = "MSG_CHOOSE_MSG_TO_SEND";
        public static string MSG_TYPE_CUSTOMIZED_MSG = "MSG_TYPE_CUSTOMIZED_MSG";
        public static string MSG_TO_CANCEL = "MSG_TO_CANCEL";
        public static string BTN_GIVE_MONEY_MSG1 = "BTN_GIVE_MONEY_MSG1";
        public static string BTN_GIVE_MONEY_MSG2 = "BTN_GIVE_MONEY_MSG2";
        public static string BTN_GIVE_MONEY_MSG3 = "BTN_GIVE_MONEY_MSG3";
        public static string BTN_GIVE_MONEY_MSG4 = "BTN_GIVE_MONEY_MSG4";
        public static string MSG_AMOUNT = "MSG_AMOUNT";
        public static string MSG_123_OR_CUSTOMIZED = "MSG_123_OR_CUSTOMIZED";
        public static string BTN_SEND_MSG_MSG0 = "BTN_SEND_MSG_MSG0";
        public static string BTN_SEND_MSG_MSG1 = "BTN_SEND_MSG_MSG1";
        public static string BTN_SEND_MSG_MSG2 = "BTN_SEND_MSG_MSG2";
        public static string BTN_SEND_MSG_MSG3 = "BTN_SEND_MSG_MSG3";
        public static string BTN_SEND_MSG_MSG4 = "BTN_SEND_MSG_MSG4";
        public static string BTN_EMPTY_MSG_TO_CANCEL = "BTN_EMPTY_MSG_TO_CANCEL";
        public static string BTN_GET_REVENUE_MSG1 = "BTN_GET_REVENUE_MSG1";
        public static string BTN_GET_REVENUE_MSG2 = "BTN_GET_REVENUE_MSG2";
        public static string BTN_GET_REVENUE_MSG3 = "BTN_GET_REVENUE_MSG3";
        public static string BTN_SUBSIDIZE_MSG1 = "BTN_SUBSIDIZE_MSG1";
        public static string BTN_SUBSIDIZE_MSG3 = "BTN_SUBSIDIZE_MSG3";
        public static string MSG_WARNING = "MSG_WARNING";
        public static string PLR_OVERFLOW_WARNING = "PLR_OVERFLOW_WARNING";
        public static string PLR_EMPTY_BASKET = "PLR_EMPTY_BASKET";
        public static string PLR_INSUFFICIENT_BUDGET = "PLR_INSUFFICIENT_BUDGET";
        public static string PLR_VALIDATE_BASKET = "PLR_VALIDATE_BASKET";
        public static string PLR_CHECK_BOX_VALIDATE = "PLR_CHECK_BOX_VALIDATE";
        public static string MSG_COST_APPLIED_PARCEL = "MSG_COST_APPLIED_PARCEL";
        public static string MSG_COST_ACTION = "MSG_COST_ACTION";
        public static string MSG_COST_EXPROPRIATION = "MSG_COST_EXPROPRIATION";
        public static string LEGEND_UNAM = "LEGEND_UNAM";
        public static string LEGEND_DYKE = "LEGEND_DYKE";
        public static string LEGEND_NAME_ACTIONS = "LEGEND_NAME_ACTIONS";
        public static string MSG_INITIAL_BUDGET = "MSG_INITIAL_BUDGET";
        public static string MSG_REMAINING_BUDGET = "MSG_REMAINING_BUDGET";
        public static string MSG_SIM_STARTED_ROUND1 = "MSG_SIM_STARTED_ROUND1";
        public static string MSG_POSSIBLE_REGLEMENTATION_DELAY = "MSG_POSSIBLE_REGLEMENTATION_DELAY";
        public static string MSG_ITS_ROUND = "MSG_ITS_ROUND";
        public static string MSG_THE_ROUND = "MSG_THE_ROUND";
        public static string MSG_HAS_STARTED = "MSG_HAS_STARTED";
        public static string MSG_TAXES_RECEIVED_FROM = "MSG_TAXES_RECEIVED_FROM";
        public static string MSG_DISTRICT_RECEIVE = "MSG_DISTRICT_RECEIVE";
        public static string MSG_NEW_COMERS = "MSG_NEW_COMERS";
        public static string MSG_DISTRICT_POPULATION = "MSG_DISTRICT_POPULATION";
        public static string MSG_INHABITANTS = "MSG_INHABITANTS";
        public static string MSG_EXPROPRIATION_PROCEDURE = "MSG_EXPROPRIATION_PROCEDURE";
        public static string MSG_IMPOSSIBLE_DELETE_ADAPTED = "MSG_IMPOSSIBLE_DELETE_ADAPTED";
        public static string MSG_ROUND = "MSG_ROUND";
        public static string MSG_IMPOSSIBLE_NORMALLY = "MSG_IMPOSSIBLE_NORMALLY";
        public static string MSG_FLOODED_AREA_DISTRICT = "MSG_FLOODED_AREA_DISTRICT";
        public static string MSG_START_SENDER = "MSG_START_SENDER";
        public static string MSG_SEND_DATA_TO = "MSG_SEND_DATA_TO";
        public static string MSG_SEND_TO = "MSG_SEND_TO";
        public static string MSG_SIMULATION_CHOICE = "MSG_SIMULATION_CHOICE";
        public static string MSG_SEND_MSG_LEADER = "MSG_SEND_MSG_LEADER";
        public static string MSG_READ_MESSAGE = "MSG_READ_MESSAGE";



        public static string GetLangueElement(string lng)
        {
            string langue_value = "!!";
            current_langue.TryGetValue(lng, out langue_value);
            Debug.Log("-----------_>>>> search for " + lng);
            return langue_value;
        }


        public static void GetAllAsVariables()
        {
            string all = "";
            foreach (KeyValuePair<string, string> lng in current_langue)
            {

                all += "\n public static string " + lng.Key + " = \"" + lng.Key + "\";";
            }

            Debug.Log(all);
        }

        public ILangue()
        {

        }
    }
}
