using Restaurant.Core;
using Restaurant.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteInterfaz
{
    public class NegocioEspecifico
    {


        MetodoNegocio mn = new();

        Carrito Carrito = new();



        public NegocioEspecifico(Carrito car)
        {
            Carrito = car;
        }

        public bool IngresarCarritoDB()
        {
            int IdPedido = mn.GenerateId("IDPEDIDO", "PEDIDO");

            Pedido ped = new()
            {
                fecha = DateTime.Now,
                IdEstadoPedido = "SEXO",
                TotalIVA = (int)Carrito.GetCarritoIVA(),
                IdMesa = 7,
                IdPedido = IdPedido,
                TotalBruto = (int)Carrito.GetCarritoBruto(),
                TotalNeto = (int)Carrito.GetCarritoNeto(),
            };

            if (mn.InsertarPedido(ped))
            {
                foreach (var item in Carrito.GetCarritos())
                {
                    DetallePedido dp = new()
                    {
                        Cantidad = item.Cantidad,
                        EstadoPedido = 0,
                        IdPedido = IdPedido,
                        IdProducto = item.IdPlatoCarrito
                    };

                    if (!mn.InsertarDetPedido(dp))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
