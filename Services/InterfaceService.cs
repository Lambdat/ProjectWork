using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    //Questa interfaccia fungerà da contratto, che i vari services
    //coinvolti implementeranno
    public interface InterfaceService<T>
    {
        public List<T> GetAll();

        public T Search(int id);
        public T Search(string ssn);

        public T Add(T item);

        public T Delete(int id);

        public T Delete(string ssn);

        public T Update(int id,T item);
        public T Update(string ssn,T item);












    }
}
