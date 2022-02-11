using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public  class Carrito
    {
        public List<PlatoCarrito> platos = new();

        public void VaciarCarrito()
        {
            platos.Clear(); 
        }


        public void AddPlato(PlatoCarrito pla)
        {
           platos.Add(pla); 
        }

        public List<PlatoCarrito> GetCarritos()
        {
            return platos;
        }


        public void DeletePlato(PlatoCarrito pla)
        {
            platos.Remove(pla); 
        }
    }
}
