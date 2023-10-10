using EvacuationProject.Models;

namespace EvacuationProject.BusinessLogic
{
    public class Administrator : IModel
    {
        private string _userName;
        private string _password;
        private int _id;

        public string Name { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public int Id { get => _id; set => _id = value; }

        public Administrator(int id = 99999, string userName = "Not registered", string password = "Default")
        {
            Id = id;
            Name = userName;            
            Password = password;
        }

        public override string ToString()
        {
            return $"{_userName},{_id},{_password}";
        }
    }
}