using System.Collections.Generic;

namespace ProjectWork.Services
{
    //Questa interfaccia fungerà da contratto, che i vari services
    //coinvolti implementeranno
    public interface InterfaceService<T> // Il Generic <T> sta ad indicare qualsiasi tipo (appunto Type Generic) che sia esso un modello come un post, un gruppo, un sondaggio, etc...
    {
                                        //Il <T> va specificato per qualsiasi classe/service/dao che vada ad implementare questa interfaccia es. InterfaceService<Post>

        //Questa interfaccia fungerà da contratto, che i vari services
        //coinvolti implementeranno

        //Qui di seguito riportiamo le firme dei metodi CRUD e non solo
        public List<T> GetAll();

        public List<T> GetAllByUsername(string username);

        public T SearchById(int id);
        
        public T Add(T item);

        public T Delete(int id);

        public T UpdateById(T item);
        


    }
}
