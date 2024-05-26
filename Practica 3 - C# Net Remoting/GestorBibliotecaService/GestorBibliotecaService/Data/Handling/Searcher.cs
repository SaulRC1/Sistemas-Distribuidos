using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Data.Handling
{
    public interface Searcher<T>
    {
        List<T> Search(List<T> searched);
    }
}
