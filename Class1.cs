using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace WinFormsApp1
{
    internal class PlazoFijo
    {

        /*
         * 
         * La clase Banco es la principal del programa, ella contiene una lista de Usuarios, 
         * Caja de Ahorro, Tarjeta y Plazo Fijo
         * 
        AltaPlazoFijo(in Plazo Fijo, in Usuario) : Agrega un Plazo Fijo a la lista y lo vincula con el Usuario.
        BajaPlazoFijo(in ID): Sólo si el estado es pagado y la fecha actual es 1 mes posterior a 
            la fecha de vencimiento permite operar, esta acción elimina el registro del Plazo Fijo, 
            no es lo mismo que pagar.Elimina el Plazo Fijo de la lista del usuario y del banco.
        MostrarPlazoFijos(): Devuelve una lista con los plazo fijos del usuario actual logueado.


        Plazo Fijo: Al hacer clic se abre una ventana con el listado de sus plazos fijos y la opción para crear uno nuevo. 
        Al crear un nuevo Plazo Fijo se selecciona la cuenta de la cual se desea retirar el dinero para constituirlo, 
        si el saldo es insuficiente no permite operar. También se valida que el monto sea mínimo $1.000 para poder crearlo.
        La tasa es un parámetro fijado por el programa, el usuario la visualiza, pero no puede editarla. Al seleccionar un plazo fijo, 
        se puede eliminar del listado si el mismo ya está pagado y pasó más de un mes desde la FechaFin. OBS: Al iniciar, el sistema 
        DEBE recorrer la lista de plazo fijos, si alguno tiene FechaFin igual a la fecha de hoy, entonces debe proceder a depositar el monto final, 
        calcular monto ingresado + interés (monto * (tasa / 365) * cantidad de días) y marcar el plazo fijo como pagado. d. 
        */



        // atributos

        public int idPL { get; set; }
        public double tasaPL { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public Usuario User { get; set; }

        public double montoIN { get; set; }
        public double montoOUT { get; set; }
        public Boolean active { get; set; }

        public DateTime Hoy = DateTime.Today;

        public List<PlazoFijo> plazoFijoList = new List<PlazoFijo>();

        public static double TasaGeneral = 6.5;


        //constructor
        public PlazoFijo(int IDPL, Usuario USUARIO, double MONTOIN)
        {
            idPL = IDPL;
            tasaPL = TasaGeneral;
            fechaInicio = Hoy;
            fechaFin = new DateTime (2099, 12, 30);
            User = USUARIO;
            montoIN = MONTOIN;
            montoOUT = CalculadorIntereses(1, MONTOIN);
            active = false;

        }
        

        //metodos

        public void AltaPlazoFijo(PlazoFijo PF, Usuario USUARIO, double MontoPl)
        {
            plazoFijoList.Add(PF);
            PF.active = true;

        }

        public void BajaPlazoFijo(int ID)
        {
            foreach (PlazoFijo PF in plazoFijoList)
            {
                if (PF.idPL == ID)
                {
                    PF.active = false;
                }
               
            }

        }

        public double CalculadorIntereses(int dias, double montoIN)
        {

            //calcular monto ingresado + interés(monto * (tasa / 365) * cantidad de días)
            double TASA = (TasaGeneral / 365);
            double montoOUT = montoIN + (montoIN * TASA * dias);

            return montoOUT;

        }
        public int CalculadorDias(int dias)
        {

            

            return dias;

        }
    }
}
