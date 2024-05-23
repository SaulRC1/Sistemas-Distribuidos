using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.UserProperties
{
    public class GestorBibliotecaUserProperties
    {
        private int adminId = -1000;

        private static GestorBibliotecaUserProperties instance = new GestorBibliotecaUserProperties();

        private GestorBibliotecaUserProperties()
        {

        }

        public int AdminId
        {
            get { return this.adminId; }
            set { this.adminId = value; }
        }

        public static GestorBibliotecaUserProperties getInstance()
        {
            return instance;
        }
    }
}
