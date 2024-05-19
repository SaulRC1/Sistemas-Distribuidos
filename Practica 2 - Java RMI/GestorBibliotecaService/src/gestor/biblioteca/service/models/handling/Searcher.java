package gestor.biblioteca.service.models.handling;

import java.util.List;

/**
 *
 * @author Saúl Rodríguez Naranjo
 * @param <T>
 */
public interface Searcher <T>
{
    public List<T> search(List<T> searched);
}
