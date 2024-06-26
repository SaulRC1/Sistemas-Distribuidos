/*
 * This is sample code generated by rpcgen.
 * These are only templates and you can use them
 * as a guideline for developing your own functions.
 */

#include "GestorBiblioteca.h"
#include <time.h>
#include <stdlib.h>

//Vector dinámico de libros
TLibro *Biblioteca=NULL;

//Número de libros almacenados en el vector dinámico.
int NumLibros=0;

//Tamaño del vector dinámico. El incremento será por bloque de 4 libros.
int Tama=0;

//Copia del Identificado de Administración enviado al usuario.
int IdAdmin=-1;

//Copia del nombre del último fichero binario que se ha cargado en memoria.
Cadena NomFichero="";

//Copia del último campo de ordenación especificado.
int CampoOrdenacion=0;

static int is_random_seed_initialized = 0;

bool_t EsMenor(int P1, int P2, int Campo)
{
	bool_t salida=FALSE;
	TLibro L1=Biblioteca[P1];
	TLibro L2=Biblioteca[P2];
	
	switch(Campo)
	{
		case 0: salida=strcmp(L1.Isbn,L2.Isbn)<0?TRUE:FALSE;
				break; 
		case 1: salida=strcmp(L1.Titulo,L2.Titulo)<0?TRUE:FALSE;
				break; 
		case 2: salida=strcmp(L1.Autor,L2.Autor)<0?TRUE:FALSE;
				break; 
		case 3: salida=L1.Anio<L2.Anio?TRUE:FALSE;
				break; 
		case 4: salida=strcmp(L1.Pais,L2.Pais)<0?TRUE:FALSE;
				break; 
		case 5: salida=strcmp(L1.Idioma,L2.Idioma)<0?TRUE:FALSE;
				break; 
		case 6: salida=L1.NoLibros<L2.NoLibros?TRUE:FALSE;
				break; 
		case 7: salida=L1.NoPrestados<L2.NoPrestados?TRUE:FALSE;
				break; 
		case 8: salida=L1.NoListaEspera<L2.NoListaEspera?TRUE:FALSE;
				break; 
	}
	return salida;
}

TLibro * increment_library_size(TLibro *library, int current_size, 
	int increment)
{
	TLibro *incremented_library = realloc(library, (current_size + increment) * sizeof(TLibro));

	return incremented_library;
}

int partition(TLibro *library, int low, int high, int sorting_field)
{
	int pivot = high;

	int i = (low - 1);

	for (int j = low; j <= high; j++)
	{
		if(EsMenor(j, pivot, sorting_field) == TRUE)
		{
			i++;
			TLibro aux = library[i];
			library[i] = library[j];
			library[j] = aux;
		}
	}

	TLibro aux = library[i + 1];
	library[i + 1] = library[high];
	library[high] = aux;

	return (i + 1);
}

void quick_sort(TLibro *library, int low, int high, int sorting_field)
{
	if(low < high)
	{
		int pi = partition(library, low, high, sorting_field);

		quick_sort(library, low, pi - 1, sorting_field);
		quick_sort(library, pi + 1, high, sorting_field);
	}
}

bool_t contains(TLibro *library, TLibro book)
{
	for (int i = 0; i < NumLibros; i++)
	{
		if(strcmp(Biblioteca[i].Isbn, book.Isbn) == 0)
		{
			return TRUE;
		}	
	}

	return FALSE;
}

int *
conexion_1_svc(char *argp, struct svc_req *rqstp)
{
	static int  result;

	//Initialize random seed only once per program execution
	if (is_random_seed_initialized == 0) 
	{ 
        srand(time(NULL));
        is_random_seed_initialized = 1;
    }

	//Check if password given by the client is correct
	if(strcmp(argp, "1234") != 0)
	{
		result = -2;
		return &result;
	}

	if(IdAdmin == -1) //There is no admin connected yet
	{
		IdAdmin = 1 + rand() % RAND_MAX;
		result = IdAdmin;
	}
	else
	{
		//There is an admin connected
		result = -1;
	}

	return &result;
}

bool_t *
desconexion_1_svc(int *argp, struct svc_req *rqstp)
{
	static bool_t  result;

	if(*argp == IdAdmin)
	{
		result = TRUE;
		IdAdmin = -1;
	}
	else
	{
		result = FALSE;
	}

	return &result;
}

int *
cargardatos_1_svc(TConsulta *argp, struct svc_req *rqstp)
{
	static int  result;

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result = -1;
		return &result;
	}

	//Open file for reading binary data
	FILE *library_file = fopen(argp->Datos, "rb");

	if(library_file == NULL)
	{
		result = 0;
		return &result;
	}

	//Store the latest file opened
	strcpy(NomFichero, argp->Datos);

	//Delete the previous library content
	if(Biblioteca != NULL)
	{
		free(Biblioteca);
		Tama = 0;
		NumLibros = 0;
	}

	fread(&NumLibros, sizeof(int), 1, library_file);

	Biblioteca = malloc(NumLibros * sizeof(TLibro));
	Tama = NumLibros;

	fread(Biblioteca, sizeof(TLibro), NumLibros, library_file);

	fclose(library_file);
	result = 1;

	quick_sort(Biblioteca, 0, NumLibros - 1, CampoOrdenacion);

	return &result;
}

bool_t *
guardardatos_1_svc(int *argp, struct svc_req *rqstp)
{
	static bool_t  result;

	if(*argp != IdAdmin || *argp < 0)
	{
		result = FALSE;
		return &result;
	}

	if(Biblioteca == NULL)
	{
		result = FALSE;
		return &result;
	}

	if(NumLibros == 0)
	{
		result = FALSE;
		return &result;
	}

	if(strcmp(NomFichero, "") == 0)
	{
		result = FALSE;
		return &result;
	}

	FILE *library_file = fopen(NomFichero, "wb");

	if(library_file == NULL)
	{
		result = FALSE;
		return &result;
	}

	size_t write_number_of_books_result = 
	fwrite(&NumLibros, sizeof(int), 1, library_file);

	//If write result is less than the count of elements to be written
	//then an error has occurred.
	if(write_number_of_books_result < 1)
	{
		result = FALSE;
		fclose(library_file);
		return &result;
	}

	size_t write_books_result = 
	fwrite(Biblioteca, sizeof(TLibro), NumLibros, library_file);

	if(write_books_result < NumLibros)
	{
		result = FALSE;
		fclose(library_file);
		return &result;
	}

	result = TRUE;	

	fclose(library_file);

	return &result;
}

int *
nuevolibro_1_svc(TNuevo *argp, struct svc_req *rqstp)
{
	static int  result;

	if(Biblioteca == NULL)
	{
		result = -2;
		return &result;
	}

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result = -1;
		return &result;
	}

	bool_t library_already_contains_book = contains(Biblioteca, argp->Libro);

	if(library_already_contains_book == TRUE)
	{
		result = 0;
		return &result;
	}

	//Check if library has enough space for the new book
	if(NumLibros == Tama)
	{
		Biblioteca = increment_library_size(Biblioteca, Tama, 4);

		if(Biblioteca == NULL)
		{
			result = -2;
			return &result;
		}

		Tama = Tama + 4;
	}

	Biblioteca[NumLibros] = argp->Libro;
	NumLibros++;

	quick_sort(Biblioteca, 0, NumLibros -1, CampoOrdenacion);

	result = 1;

	return &result;
}

int *
comprar_1_svc(TComRet *argp, struct svc_req *rqstp)
{
	static int  result;

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result = -1;
		return &result;
	}

	if(Biblioteca == NULL || NumLibros == 0)
	{
		result = 0;
		return &result;
	}

	TConsulta search_book;
	search_book.Ida = argp->Ida;
	strcpy(search_book.Datos, argp->Isbn);

	int *book_position = buscar_1_svc(&search_book, rqstp);

	if(*book_position == -1)
	{
		result = 0;
		return &result;
	}

	Biblioteca[*book_position].NoLibros += argp->NoLibros;

	if(Biblioteca[*book_position].NoLibros >= Biblioteca[*book_position].NoListaEspera)
	{
		Biblioteca[*book_position].NoLibros -= Biblioteca[*book_position].NoListaEspera;
		Biblioteca[*book_position].NoListaEspera = 0;
	}
	else
	{
		Biblioteca[*book_position].NoListaEspera -= Biblioteca[*book_position].NoLibros;
		Biblioteca[*book_position].NoLibros = 0;
	}

	quick_sort(Biblioteca, 0, NumLibros - 1, CampoOrdenacion);

	result = 1;

	return &result;
}

int *
retirar_1_svc(TComRet *argp, struct svc_req *rqstp)
{
	static int  result;

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result = -1;
		return &result;
	}

	if(Biblioteca == NULL || NumLibros == 0)
	{
		result = 0;
		return &result;
	}

	TConsulta search_book;
	search_book.Ida = argp->Ida;
	strcpy(search_book.Datos, argp->Isbn);

	int *book_position = buscar_1_svc(&search_book, rqstp);

	if(*book_position == -1)
	{
		result = 0;
		return &result;
	}

	if(Biblioteca[*book_position].NoLibros >= argp->NoLibros)
	{
		Biblioteca[*book_position].NoLibros -= argp->NoLibros;
		result = 1;
	}
	else
	{
		result = 2;
	}
	
	quick_sort(Biblioteca, 0, NumLibros - 1, CampoOrdenacion);

	return &result;
}

bool_t *
ordenar_1_svc(TOrdenacion *argp, struct svc_req *rqstp)
{
	static bool_t  result;

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result = FALSE;
		return &result;
	}

	if(Biblioteca == NULL)
	{
		result = FALSE;
		return &result;
	}

	result = FALSE;

	quick_sort(Biblioteca, 0, NumLibros - 1, argp->Campo);

	CampoOrdenacion = argp->Campo;

	result = TRUE;

	return &result;
}

int *
nlibros_1_svc(int *argp, struct svc_req *rqstp)
{
	static int  result;

	result = NumLibros;

	return &result;
}

int *
buscar_1_svc(TConsulta *argp, struct svc_req *rqstp)
{
	static int  result;

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result = -2;
		return &result;
	}

	if(Biblioteca == NULL || NumLibros == 0)
	{
		result = -3;
		return &result;
	}

	//by default the book is not found
	result = -1;
	
	for (int i = 0; i < NumLibros; i++)
	{
		if(strcmp(Biblioteca[i].Isbn, argp->Datos) == 0)
		{
			result = i;
			break;		
		}
	}

	return &result;
}

TLibro *
descargar_1_svc(TPosicion *argp, struct svc_req *rqstp)
{
	static TLibro  result;

	if(argp->Pos >= NumLibros || argp->Pos < 0)
	{
		result.Anio = 0;
		strcpy(result.Autor, "????");
		strcpy(result.Idioma, "????");
		strcpy(result.Isbn, "????");
		result.NoLibros = 0;
		result.NoListaEspera = 0;
		result.NoPrestados = 0;
		strcpy(result.Pais, "????");
		strcpy(result.Titulo, "????");

		return &result;
	}

	TLibro book = Biblioteca[argp->Pos];

	result.Anio = book.Anio;
	strcpy(result.Autor, book.Autor);
	strcpy(result.Idioma, book.Idioma);
	strcpy(result.Isbn, book.Isbn);
	result.NoLibros = book.NoLibros;
	result.NoListaEspera = book.NoListaEspera;
	result.NoPrestados = book.NoPrestados;
	strcpy(result.Pais, book.Pais);
	strcpy(result.Titulo, book.Titulo);

	if(argp->Ida != IdAdmin || argp->Ida < 0)
	{
		result.NoListaEspera = 0;
		result.NoPrestados = 0;
	}

	return &result;
}

int *
prestar_1_svc(TPosicion *argp, struct svc_req *rqstp)
{
	static int  result;

	if(argp->Pos < 0 || argp->Pos >= NumLibros)
	{
		result = -1;
		return &result;
	}

	if(Biblioteca == NULL || NumLibros == 0)
	{
		result = -2;
		return &result;
	}

	TLibro book = Biblioteca[argp->Pos];

	if(book.NoLibros > 0)
	{
		book.NoLibros--;
		book.NoPrestados++;
		result = 1;
	}
	else
	{
		book.NoListaEspera++;
		result = 0;
	}

	Biblioteca[argp->Pos] = book;

	quick_sort(Biblioteca, 0, NumLibros - 1, CampoOrdenacion);

	return &result;
}

int *
devolver_1_svc(TPosicion *argp, struct svc_req *rqstp)
{
	static int  result;

	if(argp->Pos < 0 || argp->Pos >= NumLibros)
	{
		result = -1;
		return &result;
	}

	if(Biblioteca == NULL || NumLibros == 0)
	{
		result = -2;
		return &result;
	}

	TLibro book = Biblioteca[argp->Pos];

	if(book.NoListaEspera > 0)
	{
		book.NoListaEspera--;
		result = 0;
	}
	else if(book.NoListaEspera == 0 && book.NoPrestados > 0)
	{
		book.NoPrestados--;
		book.NoLibros++;
		result = 1;
	}
	else
	{
		result = 2;
	}

	Biblioteca[argp->Pos] = book;

	quick_sort(Biblioteca, 0, NumLibros - 1, CampoOrdenacion);

	return &result;
}
