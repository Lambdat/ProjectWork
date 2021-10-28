using System.Collections.Generic;

namespace ProjectWork.Services
{
    //Questa interfaccia fungerà da contratto, che i vari services
    //coinvolti implementeranno
    public interface InterfaceService<T>
    {
        //Questa interfaccia fungerà da contratto, che i vari services
        //coinvolti implementeranno

        public List<T> GetAll();

        public T SearchById(int id);
        

        public T Add(T item);

        public T Delete(int id);

        

        public T UpdateById(T item);


       












    }
}
