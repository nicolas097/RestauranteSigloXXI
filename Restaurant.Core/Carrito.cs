using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Carrito
    {
        public List<PlatoCarrito> platos = new();


        public void VaciarCarrito()
        {
            platos.Clear();
        }


        public void AddPlato(PlatoCarrito pla)
        {
            bool isAmigo = platos.Exists(x => x.Nombre == pla.Nombre);
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

        public int GetCarritoCount()
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

        public double GetCarritoNeto()
        {
            double Total = 0;

            if (platos.Count != 0)
            {
                for (int i = 0; i < platos.Count; i++)
                {
                    Total += platos[i].PrecioSubtotal;
                }
            }

            return Total;
        }

        public double GetCarritoIVA()
        {
            double IVA = 0;

            if (platos.Count != 0)
            {
                for (int i = 0; i < platos.Count; i++)
                {
                    IVA += platos[i].PrecioSubtotal * 0.19;
                }
            }

            return IVA;
        }

        public double GetCarritoBruto()
        {
            double Neto = 0;

            if (platos.Count != 0)
            {
                for (int i = 0; i < platos.Count; i++)
                {
                    Neto += platos[i].PrecioSubtotal * 0.81;
                }
            }

            return Neto;
        }


    }
}
