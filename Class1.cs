using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
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

        public Boolean dadoDeBaja { get; set; }

        public DateTime Hoy = new DateTime(1999, 12, 30);

        public List<PlazoFijo> plazoFijoList = new List<PlazoFijo>();
        public List<int> PFActivos = new List<int>();

        public static double TasaGeneral = 6.5;


        //constructor
        public PlazoFijo(int dias, double MONTOIN, Usuario USUARIO)
        {

            Hoy = DateTime.Today;
            Random rnd = new Random();

            DateTime FechaFinal = CalculadorDias(dias);
            
            idPL = rnd.Next();
            tasaPL = TasaGeneral;
            User = USUARIO;
            fechaInicio = Hoy;
            fechaFin = FechaFinal;
            montoIN = MONTOIN;
            montoOUT = CalculadorIntereses(dias, MONTOIN);
            active = false;
            dadoDeBaja = false;
            PFActivos.Add(idPL);

        }
        

        //-----------METODOS-----------

        //dar de alta pf
        public Boolean AltaPlazoFijo(PlazoFijo PF, Usuario USUARIO)
        {
            if (ConfirmarSaldo(USUARIO))
            {
                plazoFijoList.Add(PF);
                PF.active = true;
                return true;
            }
            else {
                return false;
            }

        }

        //vencimiento de pf, 

        public double RevisarVencimiento()
        {
            DHoy = DateTime.Today;
            double retorno = 0;
            foreach (PlazoFijo PF in plazoFijoList)
            {
                
                if(PF.fechaFin == DHoy)
                {
                    PF.active = false;
                    retorno = retorno + PF.montoOUT;
                    PFActivos.Remove(idPL);

                }
                
            }
            return retorno;
        }

        //baja de pf
        public void BajaPlazoFijo(int ID)
        {
            foreach (PlazoFijo PF in plazoFijoList)
            {
                if (PF.idPL == ID)
                {
                    DateTime vencido = PF.fechaFin.AddDays(30);
                    if (PF.fechaFin > vencido )
                    {
                        PF.dadoDeBaja = true;
                    }
                }
               
            }

        }

        //mostrar los plazos fijos activos
        public List<int> MostrarPlazoFijos()
        {


            return PFActivos;
        }




        /* otra version de este metodo podria ser con strings pero es mas complejo
         
         public string MostrarPlazoFijos2()
        {

            if (PFActivos.Count != 0) {
                foreach (int id in PFActivos) {

                    return id.ToString();
                } }
            else {
                string nohay = "no hay pfs activos";
                return nohay;
                    
                    }
        }
         
         */

        //calcular intereses 
        public double CalculadorIntereses(int dias, double montoIN)
        {

            //calcular monto ingresado + interés(monto * (tasa / 365) * cantidad de días)
            double TASA = (TasaGeneral / 365);
            double montoOUT = montoIN + (montoIN * TASA * dias);

            return montoOUT;

        }

        //calcular dias
        public DateTime CalculadorDias(int dias)
        {
            Hoy = DateTime.Today;
            DateTime FechaFin = Hoy.AddDays (dias);

            return FechaFin;

        }

        //confirmar monto disponible
        public Boolean ConfirmarSaldo(Usuario USR) { 
        
            if(USR.Cuenta >1000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
