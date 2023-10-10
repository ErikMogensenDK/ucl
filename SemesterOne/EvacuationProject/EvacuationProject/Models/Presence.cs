
namespace EvacuationProject.Models
{
    public class Presence
    {
        private Workstation _workstation;
        private DateTime _startTime;

        public Workstation Workstation{ get => _workstation; set => _workstation = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }

        public Presence(Workstation workstation, DateTime startTime)
        {
           Workstation = workstation; 
           StartTime = startTime;
        }

        public override string ToString()
        {
            return($"{_workstation.Id},{_startTime}");
        }
    }
}