using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaticApp.Models
{
    public class Employee
    {
        public readonly int UserId;
        public readonly string Name;
        private List<SkillLevel>? _skills;
    public Employee(int userId, string name, List<SkillLevel> skills = null)
    {
        UserId = userId;
        Name = name;

        if (skills == null)
            _skills = new();
        else
            _skills = skills;
    }
    }

}