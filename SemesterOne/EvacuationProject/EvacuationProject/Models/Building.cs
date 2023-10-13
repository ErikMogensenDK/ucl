using EvacuationProject.BusinessLogic;

namespace EvacuationProject.Models
{
    public class Building : IModel
    {
        private string _name;
        private int? _id;

        public string Name { get => _name; set => _name = value; }
        public int? Id { get => _id; set => _id = value; }

        public Building(string name, int? id = null)
        {
            Name = name;            
            Id = id;
        }
        public override string ToString()
        {
            string myIdString = "";
            if (Id == null)
                myIdString = "null";
            else
                myIdString = Id.ToString();

            return $"Building id:{myIdString},Name:{_name}";
        }
    }
    
}