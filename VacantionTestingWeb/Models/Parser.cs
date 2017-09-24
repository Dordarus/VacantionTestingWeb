using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace VacantionTestingWeb.Models
{
    //XML-doc parsing method
    public static class Parser
    {
        static private CultureInfo USCulture = new CultureInfo("en-US");

        public static Group Parse(string url)
        {
            Group group = new Group
            {
                Sessions = new List<Session>()
            };

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(url);
            XmlElement xRoot = xDoc.DocumentElement;

            XmlNode nameNode = xRoot.SelectSingleNode("//groups/group/name");
            group.Name = nameNode.InnerText;

            XmlNodeList sessionNodes = xRoot.SelectNodes("//groups/group/sessions/session");

            foreach (XmlNode sessionChildnode in sessionNodes)
            {
                Session session = new Session
                {
                    Animals = new List<Animal>()
                };

                var date = sessionChildnode.SelectSingleNode("sessiondate").InnerText;
                session.Date = DateTime.Parse(date, USCulture, DateTimeStyles.NoCurrentDateDefault);

                var animalNodes = sessionChildnode.SelectNodes("animals/animal");
                foreach (XmlNode animalChildnode in animalNodes)
                {
                    Animal animal = new Animal();
                    var value = animalChildnode.SelectSingleNode("data/datum/value").InnerText;
                    animal.Value = Convert.ToDouble(value, USCulture);

                    session.Animals.Add(animal);
                }
                session.AvgValue = AvgValue(session);

                group.Sessions.Add(session);
            }
            return group;
        }

        //Average counting function
        private static double AvgValue(Session session)
        {
            double sum = 0;
            int count = session.Animals.Count;

            foreach(var animal in session.Animals)
            {
                sum += animal.Value;
            }
            return sum / count;
        }      
    }
}