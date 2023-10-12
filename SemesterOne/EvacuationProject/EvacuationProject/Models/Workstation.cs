namespace EvacuationProject.Models
{
    public class Workstation : IModel
    {
        private string _name;
        private int _id;
        private Room _room;

        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }
        public Room Room { get => _room; set => _room = value; }

        public Workstation(string name, int id, Room room)
        {
            Name = name;
            Id = id;
            Room = room;
        }
        public override string ToString()
        {
            return $"Name:{_name},Id:{_id},Room id:{_room.Id}";
        }
    }

}