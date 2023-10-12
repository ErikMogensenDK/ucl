using System;
using EvacuationProject.BusinessLogic;

namespace EvacuationProject.Models
{
    public class User : IModel
    {
        private int _id;
        private string _name;
        private AccessLevel _accessLevel;
        private Presence? _presence;

        public int Id{ get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public AccessLevel AccessLevel { get => _accessLevel; set => _accessLevel = value; }
        public Presence Presence { get => _presence; set => _presence = value; }

        public User(int userId= 0, string name = "myname", AccessLevel accessLevel = AccessLevel.Employee, Presence presence = null) 
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
                stringPresence = "null";
            else
                stringPresence = $"{_presence.ToString()}";
            return $"Name:{_name},Id:{_id},Access level:{_accessLevel},{stringPresence}";
        }
    }
}