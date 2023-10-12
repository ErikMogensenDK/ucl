namespace EvacuationProject.Models
{
    public class Room : IModel
    {
        private string _name;
        private int _id;
        private int _floor;
        private Building _building;

        public string Name { get => _name; set => _name = value; }
        public Building Building { get => _building; set => _building = value; }
        public int Floor { get => _floor; set => _floor = value; }
        public int Id { get => _id; set => _id= value; }

        public Room(string name, int id, int floor, Building building)
        {
            Building = building;
            Name = name;
            Id = id;
            Floor = floor;
        }
        public override string ToString()
        {
            return $"Name:{_name},Id:{_id},Floor:{_floor},Building id:{_building.Id}";
        }
    }
}