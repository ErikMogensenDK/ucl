using System;
using EvacuationProject.BusinessLogic;

namespace EvacuationProject.Models
{
    public class User : IModel
    {
        private int? _id;
        private string _name;
        private AccessLevel _accessLevel;
        private Presence? _presence;

        public int? Id{ get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public AccessLevel AccessLevel { get => _accessLevel; set => _accessLevel = value; }
        public Presence Presence { get => _presence; set => _presence = value; }
        public int? EmployeeId { get => _id;}

        public User(int? userId= null, string name = "myname", AccessLevel accessLevel = AccessLevel.Employee, Presence presence = null) 
        {
            Id = userId;
            Name = name;
            AccessLevel = accessLevel;
            Presence = presence;
        }

        public override string ToString()
        {
            string stringPresence;
            if (_presence == null)
                stringPresence = "Presence:null";
            else
                stringPresence = $"Presence:{_presence.ToString()}";
            return $"Employee id:{_id},Name:{_name},Access level:{_accessLevel},{stringPresence}";
        }
    }
}