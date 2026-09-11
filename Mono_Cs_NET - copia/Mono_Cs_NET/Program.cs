using System;
using System.Windows.Forms;

namespace HoribaTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TestForm());
        }
    }

    public class TestForm : Form
    {
        public TestForm()
        {
            Text = "HORIBA Test";
            Width = 500;
            Height = 300;

            Load += TestForm_Load;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("1. Creando ConfigBrowser...");

                JYCONFIGBROWSERCOMPONENTLib.JYConfigBrowerInterface config =
                    new JYCONFIGBROWSERCOMPONENTLib.JYConfigBrowerInterface();

                Console.WriteLine("2. ConfigBrowser creado");

                config.Load();

                Console.WriteLine("3. ConfigBrowser.Load() OK");

                string name;

                string id = config.GetFirstMono(out name);

                Console.WriteLine(
                    "4. GetFirstMono: ID = {0}, Name = {1}",
                    id,
                    name
                );

                Console.WriteLine("5. Creando MonochromatorClass...");

                JYMONOLib.MonochromatorClass mono =
                    new JYMONOLib.MonochromatorClass();

                Console.WriteLine("6. MonochromatorClass creado");

                mono.Uniqueid = id;

                Console.WriteLine("7. Uniqueid asignado");

                mono.Load();

                Console.WriteLine("8. mono.Load() OK");

                mono.OpenCommunications();

                Console.WriteLine("9. OpenCommunications() OK");

                Console.WriteLine(
                    "10. Wavelength = {0}",
                    mono.GetCurrentWavelength()
                );

                MessageBox.Show(
                    "HORIBA test terminado correctamente.",
                    "OK"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "ERROR"
                );
            }
        }
    }
}