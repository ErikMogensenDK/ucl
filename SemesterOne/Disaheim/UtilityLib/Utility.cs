using Disaheim;

namespace UtilityLib
{
    public class Utility
    {
        public double GetValueOfBook(Book book)
        {
            return book.Price;
        }
        public double GetValueOfAmulet(Amulet amulet)
        {
            if (amulet.Quality == Level.low)
                return 12.5;
            else if (amulet.Quality == Level.medium)
                return 20.0;
            else 
                return 27.5;
        }
        public double GetValueOfCourse(Course course)
        {
            double priceOfStartedhours = 875.0;
            double startedHours = Math.Floor(course.DurationInMinutes / 60.0);
            int remainder = course.DurationInMinutes % 60;
            if (remainder > 0)
                startedHours +=1;
            return startedHours * priceOfStartedhours;
        }
    }
}