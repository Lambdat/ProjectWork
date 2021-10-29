using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public class UserService : InterfaceService<User>
    {
        //Questo metodo non lo implementiamo per il momento dato che
        //già nell'AuthService avviene la Registrazione/Creazione
        //
        public User Add(User item)
        {
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User SearchById(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateById(User item)
        {
            throw new NotImplementedException();
        }
    }
}
