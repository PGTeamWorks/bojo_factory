using System.Collections.Generic;

namespace Dominio.Interfaces
{
    public interface IRepositorioBase<T> where T: class 
    {
       T ManipulaObjeto(T obj);

        T ObterPorId(int id);

       IEnumerable<T> Obter();
        
    }
}
