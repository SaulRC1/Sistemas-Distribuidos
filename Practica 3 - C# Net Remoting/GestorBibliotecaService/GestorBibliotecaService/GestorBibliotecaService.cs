using GestorBibliotecaService.Data;
using GestorBibliotecaService.Data.Handling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    public class GestorBibliotecaService : MarshalByRefObject, GestorBibliotecario_inf
    {
        private int adminId = -1;

        private const string adminPassword = "1234";

        private List<TDatosRepositorio> loadedRepositories = new List<TDatosRepositorio>();

        private int sortingField = BookComparer.ISBN_SORTING_FIELD;

        private List<TLibro> generalBookStorage = new List<TLibro>();

        public int AbrirRepositorio(int pIda, string pNomFichero)
        {
            if (pIda != adminId)
            {
                return -1;
            }

            FileStream fs = null;
            BinaryReader binaryReader = null;

            try
            {
                fs = File.Open(pNomFichero, FileMode.Open);

                binaryReader = new BinaryReader(fs, Encoding.UTF8, false);

                int numberOfBooks = binaryReader.ReadInt32();
                string repositoryName = binaryReader.ReadString();
                string repositoryAddress = binaryReader.ReadString();

                BookRepository bookRepository = new InMemoryBookRepository();

                TDatosRepositorio repositoryData = new TDatosRepositorio(repositoryName,
                    repositoryAddress, numberOfBooks, bookRepository, pNomFichero);

                if (loadedRepositories.Contains(repositoryData))
                {
                    return -2;
                }

                for (int i = 0; i < numberOfBooks; i++)
                {
                    string isbn = binaryReader.ReadString();
                    string title = binaryReader.ReadString();
                    string author = binaryReader.ReadString();
                    int year = binaryReader.ReadInt32();
                    string country = binaryReader.ReadString();
                    string language = binaryReader.ReadString();
                    int available = binaryReader.ReadInt32();
                    int borrowed = binaryReader.ReadInt32();
                    int booked = binaryReader.ReadInt32();

                    TLibro book = new TLibro(title, author, country, language, isbn, year, available, borrowed, booked);

                    repositoryData.BookRepository.AddBook(book);
                    generalBookStorage.Add(book);
                }

                loadedRepositories.Add(repositoryData);

                Ordenar(pIda, sortingField);

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                if (binaryReader != null)
                {
                    binaryReader.Close();
                    binaryReader.Dispose();
                }
            }

        }

        public int Buscar(int pIda, string pIsbn)
        {
            if (pIda != adminId)
            {
                return -2;
            }

            for (int i = 0; i < generalBookStorage.Count(); i++)
            {
                TLibro book = generalBookStorage.ElementAt(i);

                if (pIsbn == book.Isbn)
                {
                    return i;
                }
            }

            return -1;
        }

        public int Comprar(int pIda, string pIsbn, int pNoLibros)
        {
            if (pIda != adminId)
            {
                return -1;
            }

            int bookPosition = Buscar(pIda, pIsbn);

            if (bookPosition < 0)
            {
                return 0;
            }

            TLibro book = this.generalBookStorage.ElementAt(bookPosition);

            book.Disponibles = book.Disponibles + pNoLibros;

            if (book.Disponibles >= book.Reservados)
            {
                book.Disponibles = book.Disponibles - book.Reservados;
                book.Prestados = book.Prestados + book.Reservados;
                book.Reservados = 0;
            }
            else
            {
                book.Reservados = book.Reservados - book.Disponibles;
                book.Prestados = book.Prestados + book.Disponibles;
                book.Disponibles = 0;
            }

            Ordenar(pIda, sortingField);

            return 1;
        }

        public int Conexion(string pPasswd)
        {
            if (adminId >= 1)
            {
                //There is and admin identified already
                return -1;
            }

            if (pPasswd != adminPassword)
            {
                //Wrong password
                return -2;
            }

            Random random = new Random();

            //random number between 1 and 1.000.000
            adminId = random.Next(1, 1000001);

            return adminId;
        }

        public TDatosRepositorio DatosRepositorio(int pIda, int pRepo)
        {
            if (pIda != adminId)
            {
                return null;
            }

            if (pRepo < 0 || pRepo >= this.loadedRepositories.Count())
            {
                return null;
            }

            return this.loadedRepositories.ElementAt(pRepo);
        }

        public TLibro Descargar(int pIda, int pRepo, int pPos)
        {
            if (pRepo != -1 && pRepo >= this.loadedRepositories.Count())
            {
                return null;
            }

            if (pRepo == -1)
            {
                if (pPos < 0 || pPos >= this.generalBookStorage.Count())
                {
                    return null;
                }

                TLibro generalBook = (TLibro)this.generalBookStorage.ElementAt(pPos).Clone();

                if (pIda != adminId)
                {
                    generalBook.Prestados = 0;
                    generalBook.Reservados = 0;
                }

                return generalBook;
            }

            TDatosRepositorio repositoryData = this.loadedRepositories.ElementAt(pRepo);

            if (pPos < 0 || pPos >= repositoryData.BookRepository.GetNumberOfBooks())
            {
                return null;
            }

            TLibro book = (TLibro)repositoryData.BookRepository.GetAllBooks().ElementAt(pPos).Clone();

            if (pIda != adminId)
            {
                book.Prestados = 0;
                book.Reservados = 0;
            }

            return book;
        }

        public bool Desconexion(int pIda)
        {
            if (pIda != adminId)
            {
                return false;
            }

            adminId = -1;

            return true;
        }

        public int Devolver(int pPos)
        {
            if (pPos < 0 || pPos >= this.generalBookStorage.Count())
            {
                return -1;
            }

            TLibro book = this.generalBookStorage.ElementAt(pPos);

            if (book.Reservados > 0)
            {
                book.Reservados = book.Reservados - 1;
                Ordenar(adminId, sortingField);
                return 0;
            }

            if (book.Reservados == 0 && book.Prestados > 0)
            {
                book.Prestados = book.Prestados - 1;
                book.Disponibles = book.Disponibles + 1;
                Ordenar(adminId, sortingField);
                return 1;
            }

            return 2;
        }

        public int GuardarRepositorio(int pIda, int pRepo)
        {
            if (pIda != adminId)
            {
                return -1;
            }

            if (pRepo < -1 || pRepo >= this.loadedRepositories.Count())
            {
                return -2;
            }

            try
            {
                if (pRepo == -1)
                {
                    foreach (TDatosRepositorio repository in loadedRepositories)
                    {
                        using (FileStream stream = File.Open(repository.RepositoryFilePath, FileMode.Create))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false))
                            {
                                writer.Write(repository.NumberOfBooks);
                                writer.Write(repository.RepositoryName);
                                writer.Write(repository.RepositoryAddress);

                                List<TLibro> repositoryBooks = repository.BookRepository.GetAllBooks();

                                for (int i = 0; i < repositoryBooks.Count(); i++)
                                {
                                    TLibro book = repositoryBooks.ElementAt(i);

                                    writer.Write(book.Isbn);
                                    writer.Write(book.Titulo);
                                    writer.Write(book.Autor);
                                    writer.Write(book.Anio);
                                    writer.Write(book.Pais);
                                    writer.Write(book.Idioma);
                                    writer.Write(book.Disponibles);
                                    writer.Write(book.Prestados);
                                    writer.Write(book.Reservados);
                                }
                            }
                        }
                    }
                }
                else
                {
                    TDatosRepositorio repository = this.loadedRepositories.ElementAt(pRepo);

                    using (FileStream stream = File.Open(repository.RepositoryFilePath, FileMode.Create))
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false))
                        {
                            writer.Write(repository.NumberOfBooks);
                            writer.Write(repository.RepositoryName);
                            writer.Write(repository.RepositoryAddress);

                            List<TLibro> repositoryBooks = repository.BookRepository.GetAllBooks();

                            for (int i = 0; i < repositoryBooks.Count(); i++)
                            {
                                TLibro book = repositoryBooks.ElementAt(i);

                                writer.Write(book.Isbn);
                                writer.Write(book.Titulo);
                                writer.Write(book.Autor);
                                writer.Write(book.Anio);
                                writer.Write(book.Pais);
                                writer.Write(book.Idioma);
                                writer.Write(book.Disponibles);
                                writer.Write(book.Prestados);
                                writer.Write(book.Reservados);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return 0;
            }

            return 1;
        }

        public int NLibros(int pRepo)
        {
            if (pRepo != -1 && pRepo >= this.loadedRepositories.Count())
            {
                return -1;
            }

            if (pRepo == -1)
            {
                int numberOfBooks = 0;

                foreach (TDatosRepositorio repositoryData in loadedRepositories)
                {
                    numberOfBooks += repositoryData.BookRepository.GetNumberOfBooks();
                }

                return numberOfBooks;
            }

            return this.loadedRepositories.ElementAt(pRepo).BookRepository.GetNumberOfBooks();
        }

        public int NRepositorios(int pIda)
        {
            if (pIda != adminId)
            {
                return -1;
            }

            return this.loadedRepositories.Count();
        }

        public int NuevoLibro(int pIda, TLibro L, int pRepo)
        {
            if (pIda != adminId)
            {
                return -1;
            }

            if (pRepo < 0 || pRepo >= this.loadedRepositories.Count())
            {
                return -2;
            }

            if (this.generalBookStorage.Contains(L))
            {
                return 0;
            }

            TDatosRepositorio repositoryData = this.loadedRepositories.ElementAt(pRepo);

            repositoryData.BookRepository.AddBook(L);
            repositoryData.NumberOfBooks++;

            this.generalBookStorage.Add(L);

            Ordenar(pIda, sortingField);

            return 1;
        }

        public bool Ordenar(int pIda, int pCampo)
        {
            if (pIda != adminId)
            {
                return false;
            }

            if (pCampo < BookComparer.ISBN_SORTING_FIELD || pCampo > BookComparer.BOOKED_SORTING_FIELD)
            {
                return false;
            }

            BookComparer bookComparer = new BookComparer(pCampo);

            this.generalBookStorage.Sort(bookComparer);

            foreach (TDatosRepositorio repositoryData in loadedRepositories)
            {
                repositoryData.BookRepository.SortBooks(bookComparer);
            }

            this.sortingField = pCampo;

            return true;
        }

        public int Prestar(int pPos)
        {
            if (pPos < 0 || pPos >= this.generalBookStorage.Count())
            {
                return -1;
            }

            TLibro book = this.generalBookStorage.ElementAt(pPos);

            if (book.Disponibles > 0)
            {
                book.Disponibles = book.Disponibles - 1;
                book.Prestados = book.Prestados + 1;
                Ordenar(adminId, sortingField);
                return 1;
            }
            else
            {
                book.Reservados = book.Reservados + 1;
                Ordenar(adminId, sortingField);
                return 0;
            }
        }

        public int Retirar(int pIda, string pIsbn, int pNoLibros)
        {
            if (pIda != adminId)
            {
                return -1;
            }

            int bookPosition = Buscar(pIda, pIsbn);

            if (bookPosition < 0)
            {
                return 0;
            }

            TLibro book = this.generalBookStorage.ElementAt(bookPosition);

            if (book.Disponibles >= pNoLibros)
            {
                book.Disponibles = book.Disponibles - pNoLibros;

                Ordenar(pIda, sortingField);

                return 1;
            }

            return 2;
        }
    }
}
