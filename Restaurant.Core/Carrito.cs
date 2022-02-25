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
            bool isAmigo = platos.Exists(x=> x.Nombre == pla.Nombre);
            if (platos.Count != 0)
            {
                if (isAmigo)
                {
                    for (int i = platos.Count - 1; i >= 0; i--)
                    {
                        if (platos[i].Nombre == pla.Nombre)
                        {
                            platos[i].Cantidad++;
                        }
                    }
                }
                else
                {
                    platos.Add(pla);
                }
            }
            else
            {
                platos.Add(pla);
            }
        }

        public List<PlatoCarrito> GetCarritos()
        {
            return platos;
        }


        public void DeletePlato(PlatoCarrito pla)
        {
            platos.Remove(pla); 
        }

        public int GetPCCount()
        {

            int CountPla = 0;

            if (platos.Count != 0)
            {
                for (int i = 0; i < platos.Count; i++)
                {
                    CountPla += platos[i].Cantidad;
                }
            }



            return CountPla;
        }

    }
}
