using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBuy.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }

        //Como o usuário pode ter nenhum ou mais de 1 pedido adiciona-se uma coleção de pedidos
        public ICollection<Pedido> Pedidos { get; set; } 

    }
}
