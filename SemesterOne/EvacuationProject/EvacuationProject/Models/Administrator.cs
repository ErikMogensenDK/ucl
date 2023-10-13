using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public class Administrator : IModel
    {
        private string _name;
        private string _password;
        private int? _id;

        public string Name { get => _name; set => _name = value; }
        public string Password { get => _password; set => _password = value; }
        public int? Id { get => _id; set => _id = value; }

        public Administrator(int? id = null, string userName = "Not registered", string password = "Default")
        {
            Id = id;
            Name = userName;            
            Password = password;
        }

        public override string ToString()
        {
            return $"Administrator id:{_id},Name:{_name},Password:{_password}";
        }
    }
}