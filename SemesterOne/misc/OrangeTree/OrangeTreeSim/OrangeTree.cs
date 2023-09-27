using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeTreeSim
{
    public class OrangeTree
    {
        private int _age = 0;
        public int Age
        {
            get { return _age; }
            private set
            {
                if(value > 0)
                    _age = value;
            }
        }
        private int _height = 6;
        public int Height { get{return _height;} private set{_height = value;} }
        private bool _treeAlive = true;
        public bool TreeAlive { get { return _treeAlive; } private set { _treeAlive = value; } }
        private int _numOranges;
        public int NumOranges { get{return _numOranges;} set{;} }
        private int _orangesEaten;
        public int OrangesEaten { get{return _orangesEaten;} private set{_orangesEaten = value;} }

        public void OneYearPasses()
        {
            _age++;
            if (_age < 80)
                _height += 2;
            if (_age <= 1)
                _numOranges = 0;
            else
                _numOranges = (_age - 1) * 5;
            if (_age >= 80)
            {
                _treeAlive = false;
                _numOranges = 0;
            }
            _orangesEaten = 0;
        }
        public void EatOrange(int count)
        {
            if (count <= _numOranges)
            {
                _numOranges -= count;
                _orangesEaten += count;
            }
        }
    }
}