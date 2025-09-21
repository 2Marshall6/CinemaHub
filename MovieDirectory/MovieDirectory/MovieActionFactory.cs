using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class MovieActionFactory
    {
        public static IActionMovie<T> CreateAction<T>(ActionType type)
        {
            return type switch
            {
                ActionType.Create => new Create() as IActionMovie<T>,
                ActionType.Update => new Update() as IActionMovie<T>,
                ActionType.Delete => new Delete() as IActionMovie<T>,
                _ => throw new ArgumentException("Invalid strategy type")
            };
        }
    }
}
