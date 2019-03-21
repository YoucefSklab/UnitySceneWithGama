using System;
namespace ummisco.gama.unity.littosim
{
    public class ILangue
    {
        public static string ACTION_REPAIR_DIKE = "Renover une digue";
        public static string ACTION_CREATE_DIKE = "Construire une digue";
        public static string ACTION_DESTROY_DIKE = "Dementeler une digue";
        public static string ACTION_RAISE_DIKE = "Rehausser une digue";
        public static string ACTION_INSTALL_GANIVELLE = "Installer des ganivelles";
        public static string ACTION_MODIFY_LAND_COVER_AU = "Changer en zone a urbaniser";
        public static string ACTION_MODIFY_LAND_COVER_A = "Changer en zone agricole";
        public static string ACTION_MODIFY_LAND_COVER_U = "Cliquez sur la cellule a modifier";
        public static string ACTION_MODIFY_LAND_COVER_N = "Changer en zone naturelle";
        public static string ACTON_MODIFY_LAND_COVER_FROM_AU_TO_N = "Changer une zone a urbaniser en zone naturelle";
        public static string ACTON_MODIFY_LAND_COVER_FROM_A_TO_N = "Changer une zone agricole en zone naturelle";
        public static string ACTION_MODIFY_LAND_COVER_AUs = "Changer en zone a urbaniser adaptee";
        public static string ACTION_MODIFY_LAND_COVER_Us = "Transformer en Us";
        public static string ACTION_MODIFY_LAND_COVER_Ui = "Inciter a la densification";
        public static string ACTION_EXPROPRIATION = "Cliquez sur la cellule a modifier";
        public static string ACTION_MODIFY_LAND_COVER_AUs_SUBSIDY = "Changer en zone a urbaniser adaptee avec subvention";
        public static string ACTION_MODIFY_LAND_COVER_Us_SUBSIDY = "Changer en zone urbanisee adaptee avec subvention";
        public static string ACTION_INSPECT_LAND_USE = "Inspecter une unite d'amenagement";
        public static string ACTION_INSPECT_DIKE = "Inspecter un ouvrage de defense";
        public static string ACTION_DISPLAY_PROTECTED_AREA = "Afficher les zones protegees";
        public static string ACTION_DISPLAY_FLOODED_AREA = "Afficher les zones innondees";
        public static string MSG_NEW_ROUND = "Nouveau tour de jeu";
        public static string MSG_GAME_DONE = "Termine";
        public static string MSG_LOG_USER_ACTION = "Enregistrer les actions utilisateur";
        public static string MSG_CONNECT_ACTIVMQ = "Connecter ActiveMQ";
        public static string MSG_SIM_NOT_STARTED = "La simulation n'a pas encore commence";
        public static string MSG_NO_FLOOD_FILE_EVENT = "Il n'y a pas de fichier de resultat Lisflood pour cet evenement";
        public static string MSG_OK_CONTINUE = "Cliquez sur OK pour continuer";
        public static string MSG_SUBMERSION_NUMBER = "Indiquer le numero de la submersion que vous voulez reafficher";
        public static string MSG_NUMBER = "Numero";
        public static string HELP_MSG_REPAIR_DIKE = "Cliquez sur la digue a renover";
        public static string HELP_MSG_CREATE_DIKE = "Cliquez aux deux extremites du lineaire de digue";
        public static string HELP_MSG_DESTROY_DIKE = "Cliquez sur la digue a dementeler";
        public static string HELP_MSG_RAISE_DIKE = "Cliquer sur la digue a rehausser";
        public static string HELP_MSG_INSTALL_GANIVELLE = "Cliquez sur la dune pour installer une ganivelle";
        public static string HELP_MSG_MODIFY_LAND_COVER_AU = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_A = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_U = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_N = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_FROM_AU_TO_N = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_FROM_A_TO_N = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_AUs = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_Us = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_Ui = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_EXPROPRIATION = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_AUs_SUBSIDY = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_MODIFY_LAND_COVER_Us_SUBSIDY = "Cliquez sur la cellule a modifier";
        public static string HELP_MSG_INSPECT_LAND_USE = "Glissez le pointeur sur les cellules a inspecter";
        public static string HELP_MSG_INSPECT_DIKE = "Glissez le pointeur sur les digues et dunes";
        public static string LDR_MSG_SEND_MONEY = "Envoyer de l'argent";
        public static string LDR_MSG_WITHDRAW_MONEY = "Prelever de l'argent";
        public static string LDR_MSG_SEND_MONEY_TO = "Envoyer de l'argent a";
        public static string LDR_MSG_TAKE_MONEY_FROM = "Prelever de l'argent a";
        public static string LDR_MSG_SEND_MSG = "Envoyer un message";
        public static string LDR_MSG_SEND_MSG_TO = "Envoyer un message a";
        public static string LDR_MSG_AMOUNT_REVENUE = "Montant de la recette";
        public static string LDR_MSG_AMOUNT_SUBSIDY = "Montant de la subvention";
        public static string BTN_TAKE_MONEY_MSG1 = "Les autorites reorientent leur politique: vos actions vous coutent plus cher que prevu. Vous etes preleve de";
        public static string BTN_TAKE_MONEY_MSG2 = "Les couts dans le BTP augmentent considerablement et votre budget en est impacté. Vous etes preleve de";
        public static string BTN_TAKE_MONEY_MSG3 = "L’agence du risque preleve une amende en raison de vos actions de gestion des risques. Vous etes preleve de";
        public static string BTN_TAKE_MONEY_MSG4 = "Indiquer le montant preleve a ";
        public static string MSG_CHOOSE_MSG_TO_SEND = "Choix du message envoye au joueur";
        public static string MSG_TYPE_CUSTOMIZED_MSG = "Ou tapez votre message personnalise sans utiliser de guillemets";
        public static string MSG_TO_CANCEL = "pour annuler";
        public static string BTN_GIVE_MONEY_MSG1 = "L’agence du risque finance une partie de vos pratiques de gestion integree des risques. Vous recevez";
        public static string BTN_GIVE_MONEY_MSG2 = "Les autorités subventionnent vos actions de gestion des risques. Vous recevez";
        public static string BTN_GIVE_MONEY_MSG3 = "Vos efforts en matiere de gestion des risques sont financierement encourages. Vous recevez";
        public static string BTN_GIVE_MONEY_MSG4 = "Indiquer le montant envoye a ";
        public static string MSG_AMOUNT = "Montant";
        public static string MSG_123_OR_CUSTOMIZED = "Message (1,2,3 ou message personnalise)";
        public static string BTN_SEND_MSG_MSG0 = "Indiquer le message envoye a";
        public static string BTN_SEND_MSG_MSG1 = "Un renforcement de la loi Littoral retarde vos mesures de gestion du risque";
        public static string BTN_SEND_MSG_MSG2 = "L’agence du risque desapprouve votre gestion du risque : vos dossiers sont retardes";
        public static string BTN_SEND_MSG_MSG3 = "L’agence du risque facilite vos pratiques alternatives de gestion des risques et active vos dossiers";
        public static string BTN_SEND_MSG_MSG4 = "Un changement legislatif est opere en faveur de vos pratiques de gestion des risques : vous etes encourages a poursuivre votre strategie";
        public static string BTN_EMPTY_MSG_TO_CANCEL = "message vide pour annuler";
        public static string BTN_GET_REVENUE_MSG1 = "Vous allez prelever une recette en provenance de ";
        public static string BTN_GET_REVENUE_MSG2 = "Mettre un montant de 0 pour annuler";
        public static string BTN_GET_REVENUE_MSG3 = "Montant de la recette";
        public static string BTN_SUBSIDIZE_MSG1 = "Vous allez subventionner la commune de ";
        public static string BTN_SUBSIDIZE_MSG3 = "Montant de la subvention";
        public static string MSG_WARNING = "Avertissement";
        public static string PLR_OVERFLOW_WARNING = "Vous avez atteint la capacite maximum de votre panier. Veuillez valider votre panier avant de continuer";
        public static string PLR_EMPTY_BASKET = "Votre panier est vide";
        public static string PLR_INSUFFICIENT_BUDGET = "Vous ne disposez pas du budget suffisant pour realiser toutes ces actions";
        public static string PLR_VALIDATE_BASKET = "Vous etes sur le point de valider votre panier";
        public static string PLR_CHECK_BOX_VALIDATE = "Cocher la case pour accepter le panier et valider";
        public static string MSG_COST_APPLIED_PARCEL = "Cout si applique a une parcelle";
        public static string MSG_COST_ACTION = "Cout de l'action";
        public static string MSG_COST_EXPROPRIATION = "cout d'expropriation";
        public static string LEGEND_UNAM = "Amenagement, PLU et habitat";
        public static string LEGEND_DYKE = "Defense des cotes";
        public static string LEGEND_NAME_ACTIONS = "€   Actions envisagees";
        public static string MSG_INITIAL_BUDGET = "Budget initial";
        public static string MSG_REMAINING_BUDGET = "Budget restant";
        public static string MSG_SIM_STARTED_ROUND1 = "La simulation demarre. C'est le tour 1";
        public static string MSG_POSSIBLE_REGLEMENTATION_DELAY = "La zone de travaux est soumise a des contraintes reglementaires.\nLe dossier est susceptible d’etre retarde.\nSouhaitez vous poursuivre ?";
        public static string MSG_ITS_ROUND = "C'est le tour";
        public static string MSG_THE_ROUND = "Le tour";
        public static string MSG_HAS_STARTED = "a commence";
        public static string MSG_TAXES_RECEIVED_FROM = "Vous avez perçu des impots de";
        public static string MSG_DISTRICT_RECEIVE = "Votre commune accueille";
        public static string MSG_NEW_COMERS = "nouveaux arrivants";
        public static string MSG_DISTRICT_POPULATION = "La population de votre commune est de";
        public static string MSG_INHABITANTS = "habitants";
        public static string MSG_EXPROPRIATION_PROCEDURE = "Vous allez entamer une procedure d'expropriation.\nSouhaitez-vous continuer ?";
        public static string MSG_IMPOSSIBLE_DELETE_ADAPTED = "Impossible de supprimer un habitat adapte";
        public static string MSG_ROUND = "Tour";
        public static string MSG_IMPOSSIBLE_NORMALLY = "impossible normalement";
        public static string MSG_FLOODED_AREA_DISTRICT = "Surface inondee par commune";
        public static string MSG_START_SENDER = "Demarrer le sender";
        public static string MSG_SEND_DATA_TO = "Envoyer les donnees... a";
        public static string MSG_SEND_TO = "Envoyer a";
        public static string MSG_SIMULATION_CHOICE = "choix simulation";
        public static string MSG_SEND_MSG_LEADER = "envoyer un message au leader";
        public static string MSG_READ_MESSAGE = "lire message";


        public ILangue()
        {

        }
    }
}
