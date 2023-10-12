using EvacuationProject.BusinessLogic;

namespace EvacuationProject.Models
{
    public class Building : IModel
    {
        private string _name;
        private int _id;

        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }

        public Building(string name, int id)
        {
            Name = name;            
            Id = id;
        }
        public override string ToString()
        {
            return $"Name:{_name},Id:{_id}";
        }
    }
    
}