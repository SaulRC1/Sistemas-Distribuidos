/*
 * Please do not edit this file.
 * It was generated using rpcgen.
 */

#include "GestorBiblioteca.h"

bool_t
xdr_Cadena (XDR *xdrs, Cadena objp)
{
	register int32_t *buf;

	 if (!xdr_vector (xdrs, (char *)objp, 150,
		sizeof (char), (xdrproc_t) xdr_char))
		 return FALSE;
	return TRUE;
}

bool_t
xdr_TLibro (XDR *xdrs, TLibro *objp)
{
	register int32_t *buf;

	 if (!xdr_Cadena (xdrs, objp->Isbn))
		 return FALSE;
	 if (!xdr_Cadena (xdrs, objp->Titulo))
		 return FALSE;
	 if (!xdr_Cadena (xdrs, objp->Autor))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->Anio))
		 return FALSE;
	 if (!xdr_Cadena (xdrs, objp->Pais))
		 return FALSE;
	 if (!xdr_Cadena (xdrs, objp->Idioma))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->NoLibros))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->NoPrestados))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->NoListaEspera))
		 return FALSE;
	return TRUE;
}

bool_t
xdr_TNuevo (XDR *xdrs, TNuevo *objp)
{
	register int32_t *buf;

	 if (!xdr_int (xdrs, &objp->Ida))
		 return FALSE;
	 if (!xdr_TLibro (xdrs, &objp->Libro))
		 return FALSE;
	return TRUE;
}

bool_t
xdr_TComRet (XDR *xdrs, TComRet *objp)
{
	register int32_t *buf;

	 if (!xdr_int (xdrs, &objp->Ida))
		 return FALSE;
	 if (!xdr_Cadena (xdrs, objp->Isbn))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->NoLibros))
		 return FALSE;
	return TRUE;
}

bool_t
xdr_TConsulta (XDR *xdrs, TConsulta *objp)
{
	register int32_t *buf;

	 if (!xdr_int (xdrs, &objp->Ida))
		 return FALSE;
	 if (!xdr_Cadena (xdrs, objp->Datos))
		 return FALSE;
	return TRUE;
}

bool_t
xdr_TPosicion (XDR *xdrs, TPosicion *objp)
{
	register int32_t *buf;

	 if (!xdr_int (xdrs, &objp->Ida))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->Pos))
		 return FALSE;
	return TRUE;
}

bool_t
xdr_TOrdenacion (XDR *xdrs, TOrdenacion *objp)
{
	register int32_t *buf;

	 if (!xdr_int (xdrs, &objp->Ida))
		 return FALSE;
	 if (!xdr_int (xdrs, &objp->Campo))
		 return FALSE;
	return TRUE;
}