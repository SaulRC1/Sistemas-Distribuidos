using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    [ServiceContract]
    interface IGestorBiblioteca
    {
        [OperationContract]
        int Conexion(String pPasswd);

        [OperationContract]
        bool Desconexion(int pIda);

        [OperationContract]
        int NRepositorios(int pIda);

        [OperationContract]
        TDatosRepositorio DatosRepositorio(int pIda, int pRepo);

        [OperationContract]
        int AbrirRepositorio(int pIda, String pNomFichero);

        [OperationContract]
        int GuardarRepositorio(int pIda, int pRepo);

        [OperationContract]
        int NuevoLibro(int pIda, TLibro L, int pRepo);

        [OperationContract]
        int Comprar(int pIda, String pIsbn, int pNoLibros);

        [OperationContract]
        int Retirar(int pIda, String pIsbn, int pNoLibros);

        [OperationContract]
        bool Ordenar(int pIda, int pCampo);

        [OperationContract]
        int NLibros(int pRepo);

        [OperationContract]
        int Buscar(int pIda, String pIsbn);

        [OperationContract]
        TLibro Descargar(int pIda, int pRepo, int pPos);

        [OperationContract]
        int Prestar(int pPos);

        [OperationContract]
        int Devolver(int pPos);
    }
}
