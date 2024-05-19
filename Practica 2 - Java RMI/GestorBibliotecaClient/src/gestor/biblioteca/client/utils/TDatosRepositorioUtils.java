package gestor.biblioteca.client.utils;

import gestor.biblioteca.service.models.TDatosRepositorio;
import java.util.List;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class TDatosRepositorioUtils 
{
    public static void showRepositoriesList(List<TDatosRepositorio> repositories)
    {
        System.out.println("POS \t\t\tNOMBRE \t\t\tDIRECCION \t\tNº LIBROS");
        for (int i = 0; i < 93; i++)
        {
            System.out.print("*");
        }
        
        System.out.println("");
        
        for (int i = 0; i < repositories.size(); i++)
        {
            TDatosRepositorio repository = repositories.get(i);
            
            System.out.println((i + 1) + " \t\t\t" + repository.getRepositoryName() +
                    " \t\t" + repository.getRepositoryAddress() + " \t\t" +
                    repository.getNumberOfBooks());
        }
    }
    
    public static void showRepositoriesListWithAllOption(List<TDatosRepositorio> repositories)
    {
        System.out.println("POS \t\t\tNOMBRE \t\t\tDIRECCION \t\tNº LIBROS");
        for (int i = 0; i < 93; i++)
        {
            System.out.print("*");
        }
        
        System.out.println("");
        
        for (int i = 0; i < repositories.size(); i++)
        {
            TDatosRepositorio repository = repositories.get(i);
            
            System.out.println((i + 1) + " \t\t\t" + repository.getRepositoryName() +
                    " \t\t" + repository.getRepositoryAddress() + " \t\t" +
                    repository.getNumberOfBooks());
        }
        
        System.out.println(0 + " \t\t\tTodos los repositorios");
    }
}
