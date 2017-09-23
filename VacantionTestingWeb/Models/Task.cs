using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacantionTestingWeb.Models
{
    public class Group
    {
        public string Name { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
       public DateTime Date { get; set; }
       public List<Animal> Animals { get; set; }
       public double AvgValue { get; set; }
    }

    public class Animal
    {
        public double Value { get; set; }
    }
}