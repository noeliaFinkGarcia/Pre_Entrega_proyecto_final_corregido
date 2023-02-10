using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_proyecto_final
{
    public  class ProductoVendido
    {
        private long id;
        private int stock;
        private long idProducto;
        private long idVenta;

        public long Id { get => id; set => id = value; }
        public int Stock { get => stock; set => stock = value; }
        public long IdProducto { get => idProducto; set => idProducto = value; }
        public long IdVenta { get => idVenta; set => idVenta = value; }
    }
}
