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

        public T Search(string ssn);
        public T Search(int id);

        public T Add(string userSsn, T item);

        public T Delete(string ssn);
        public T Delete(string userSsn,int id);

        public T Update(string ssn,string address,string email,string phoneNumber);
        public T Update(string userSsn,T item);
        
        public List<T> GetAllPersonalPosts(string username);    //Questo metodo è un po' diverso
                                                               //da un tradizionale crud di lettura


    }
}
