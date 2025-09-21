using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class DeleteMovieModel
    {
        public DeleteMovieModel(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
