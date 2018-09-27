using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Nextzen.VectorData;
using ummisco.gama.unity.GamaAgent;
using System.Linq;

namespace ummisco.gama.unity.utils
{
    public class UtilXml
    {

        public static Vector3 vector3FromXmlNode(XmlNode[] node, string fieldName)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;
            Boolean itExist = false;

            foreach (XmlNode n in node)
            {
                if (n.Value == fieldName)
                {
                    itExist = true;
                }

                if (n.Name == "x")
                {
                    X = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (n.Name == "y")
                {
                    Y = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (n.Name == "z")
                {
                    Z = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }

            }

            if (itExist)
            {
                return new Vector3(X, Y, Z);
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }


        public static Vector3 vector3FromXmlNode(XmlNodeList node)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;


            foreach (XmlNode n in node)
            {
                if (n.Name == "x")
                {
                    X = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (n.Name == "y")
                {
                    Y = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (n.Name == "z")
                {
                    Z = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }

            }

            return new Vector3(0, 0, 0);

        }

        public static Point pointFromXmlElement(XmlNodeList listPoints)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;


            foreach (XmlElement corItem in listPoints)
            {
                if (corItem.Name == "x")
                {
                    X = float.Parse(corItem.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (corItem.Name == "y")
                {
                    Y = float.Parse(corItem.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (corItem.Name == "z")
                {
                    Z = float.Parse(corItem.InnerText); // convert the strings to float and apply to the Y variable.
                }

            }

            return new Point(X, Y, Z);
        }



        public static object valueFromXmlNode(XmlNode[] node, string fieldName)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;
            Boolean itExist = false;


            foreach (XmlNode n in node)
            {

                if (n.Value == fieldName)
                {
                    itExist = true;
                }

                if (n.Name == "x")
                {
                    X = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (n.Name == "y")
                {
                    Y = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }
                if (n.Name == "z")
                {
                    Z = float.Parse(n.InnerText); // convert the strings to float and apply to the Y variable.
                }

            }

            if (itExist)
            {
                return new Vector3(X, Y, Z);
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }


        public static Color rgbColorFromXmlNode(XmlNode[] node, string fieldName)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            Boolean itExist = false;

            foreach (XmlNode n in node)
            {
                if (n.Value == fieldName)
                {
                    itExist = true;
                }
                if (n.Name == "red")
                {
                    red = Int32.Parse(n.InnerText);
                }
                if (n.Name == "green")
                {
                    green = Int32.Parse(n.InnerText);
                }
                if (n.Name == "blue")
                {
                    blue = Int32.Parse(n.InnerText);
                }

            }

            if (itExist)
            {
                return new Color(red, green, blue);
            }
            else
            {
                return new Color(0, 0, 0);
            }
        }


        public static GamaCoordinateSequence getCoordinateSequence(XmlNodeList list)
        {
            List<Point> listPoints = new List<Point>();

            foreach (XmlElement item in list)
            {
                XmlNodeList points = item.ChildNodes;
                Point coordinates = ConvertType.pointFromXmlElement(points);
                listPoints.Add(coordinates);

            }
            GamaCoordinateSequence coordinateSequence = new GamaCoordinateSequence(listPoints);
            return coordinateSequence;
        }


        public static string getAgentName(XmlElement item)
        {
            string agentName = "";

            XmlNodeList xmlPoints = item.GetElementsByTagName("species");

            foreach (XmlElement elt in xmlPoints)
            {
                agentName += elt.InnerText;
            }

            xmlPoints = item.GetElementsByTagName("index");

            foreach (XmlElement elt in xmlPoints)
            {
                agentName += elt.InnerText;
            }

            return agentName;
        }

        public static void getValuesMapReducerAttributes(Agent gamaAgent, XmlElement item)
        {
            float value = 0;
            float falpha = 0;
            string name = "";
            XmlNodeList xml = item.GetElementsByTagName("msi.gama.util.GamaColor_-NamedGamaColor");
            foreach (XmlNode node in xml)
            {
                XmlNodeList listNode = node.ChildNodes;

                foreach (XmlNode n in listNode)
                {
                   // Debug.Log("The name is " + n.Name);
                    if (n.Name == "value")
                    {
                        value = float.Parse(n.InnerText);
                    }
                    if (n.Name == "falpha")
                    {
                        falpha = float.Parse(n.InnerText);
                    }
                    if (n.Name == "name")
                    {
                        name = n.InnerText;
                    }
                }
            }
            gamaAgent.color = new GamaColor(value, falpha, name);
        }

        public static void getEntriesAttributes(Agent gamaAgent, XmlElement item)
        {
            float value = 0;
            float falpha = 0;
            string name = "";
            XmlNodeList xml = item.GetElementsByTagName("entry");
            foreach (XmlNode node in xml)
            {
                XmlNodeList listNode = node.ChildNodes;

                int nbr = 0;
                foreach (XmlNode n in listNode)
                {
                    nbr++;
                    switch (n.InnerText)
                    {
                        case "NATURE":
                            gamaAgent.nature = listNode.Item(nbr).InnerText;
                            break;
                        case "rotation":
                            gamaAgent.rotation = vector3FromXmlNode(item.ChildNodes);
                            break;
                        case "scale":
                            gamaAgent.scale = vector3FromXmlNode(item.ChildNodes);
                            break;
                        case "type":
                            gamaAgent.type = listNode.Item(nbr).InnerText;
                            break;
                        case "speed":
                            gamaAgent.speed = float.Parse(listNode.Item(nbr).InnerText);
                            break;
                        case "hight":
                            gamaAgent.hight = Int32.Parse(listNode.Item(nbr).InnerText);
                            break;
                        default:

                            break;



                    }

                }
            }
            gamaAgent.color = new GamaColor(value, falpha, name);
        }
        public static Agent getAgent(XmlNode[] content)
        {
            Agent gamaAgent = new Agent();

            for (int i = 1; i < content.Length; i++)
            {
                XmlElement elt = (XmlElement)content.GetValue(i);
                XmlNodeList list = elt.ChildNodes;

                foreach (XmlElement item in list)
                {
                    switch (item.Name)
                    {
                        case "factory":

                            break;

                        case "SRID":

                            break;
                        case "shell":
                            gamaAgent.agentCoordinate = getCoordinateSequence(item.GetElementsByTagName("msi.gama.metamodel.shape.GamaPoint"));
                            Debug.Log("The agent coordinates ares " + gamaAgent.agentCoordinate.ToString());
                            break;
                        case "holes":

                            break;
                        case "agentReference":
                            gamaAgent.agentName = getAgentName(item);
                            Debug.Log("The agent name is " + gamaAgent.agentName);
                            break;
                        case "keysType":

                            break;
                        case "dataType":

                            break;
                        case "valuesMapReducer":
                            getValuesMapReducerAttributes(gamaAgent, item);
                            getEntriesAttributes(gamaAgent, item);
                            //Debug.Log("The agent color is is " + gamaAgent.color.ToString());
                            //Debug.Log("The agent nature " + gamaAgent.nature);
                            //Debug.Log("The agent rotation " + gamaAgent.rotation);
                            //Debug.Log("The agent type " + gamaAgent.type);
                            //Debug.Log("The agent speed " + gamaAgent.speed);
                            //Debug.Log("The agent hight " + gamaAgent.hight);

                            break;

                        default:

                            break;
                    }

                }
            }


            return gamaAgent;
        }



    }
}

