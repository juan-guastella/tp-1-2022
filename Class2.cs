using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class Usuario
    {

        public string Nombre { get; set; }
        public int idUsuario { get; set; }

        public string Password { get; set; }
        public int Cuenta { get; set; }

        public Usuario(string Nombre, int ID, string Pass, int cuenta )
        {
            this.Nombre = Nombre;
            idUsuario = ID;
            Password = Pass;
            Cuenta = cuenta;  

        }
    }
}
