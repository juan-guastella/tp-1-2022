namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            Usuario USR = new Usuario("Jorge", 123, "contra");
            PlazoFijo PF1 = new PlazoFijo(1, USR, 100);

            
            


        }
    }
}