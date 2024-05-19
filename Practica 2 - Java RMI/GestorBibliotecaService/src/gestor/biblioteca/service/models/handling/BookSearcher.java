/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package gestor.biblioteca.service.models.handling;

import gestor.biblioteca.service.models.TLibro;
import gestor.biblioteca.service.models.TLibro;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class BookSearcher implements Searcher<TLibro>
{
    public static final String ISBN_SEARCH_FIELD = "i";
    public static final String TITLE_SEARCH_FIELD = "t";
    public static final String AUTHOR_SEARCH_FIELD = "a";
    public static final String COUNTRY_SEARCH_FIELD = "p";
    public static final String LANGUAGE_SEARCH_FIELD = "d";
    public static final String ALL_SEARCH_FIELD = "*";

    private final String searchField;
    private final String searchText;

    public BookSearcher(String searchField, String searchText)
    {
        this.searchField = searchField;
        this.searchText = searchText;
    }

    @Override
    public List<TLibro> search(List<TLibro> searched)
    {
        List<TLibro> foundBooks = new ArrayList<>();
        
        if(searchField.equalsIgnoreCase(ISBN_SEARCH_FIELD))
        {
            for (TLibro book : searched)
            {
                if(book.getIsbn().contains(searchText))
                {
                    foundBooks.add(book);
                }
            }
        }
        else if(searchField.equalsIgnoreCase(TITLE_SEARCH_FIELD))
        {
            for (TLibro book : searched)
            {
                if(book.getTitulo().contains(searchText))
                {
                    foundBooks.add(book);
                }
            }
        }
        else if(searchField.equalsIgnoreCase(AUTHOR_SEARCH_FIELD))
        {
            for (TLibro book : searched)
            {
                if(book.getAutor().contains(searchText))
                {
                    foundBooks.add(book);
                }
            }
        }
        else if(searchField.equalsIgnoreCase(COUNTRY_SEARCH_FIELD))
        {
            for (TLibro book : searched)
            {
                if(book.getPais().contains(searchText))
                {
                    foundBooks.add(book);
                }
            }
        }
        else if(searchField.equalsIgnoreCase(LANGUAGE_SEARCH_FIELD))
        {
            for (TLibro book : searched)
            {
                if(book.getIdioma().contains(searchText))
                {
                    foundBooks.add(book);
                }
            }
        }
        else if(searchField.equalsIgnoreCase(ALL_SEARCH_FIELD))
        {
            for (TLibro book : searched)
            {
                if(book.getIsbn().contains(searchText) || book.getTitulo().contains(searchText)
                        || book.getAutor().contains(searchText) || book.getPais().contains(searchText)
                        || book.getIdioma().contains(searchText))
                {
                    foundBooks.add(book);
                }
            }
        }
        
        return foundBooks;
    }

}
