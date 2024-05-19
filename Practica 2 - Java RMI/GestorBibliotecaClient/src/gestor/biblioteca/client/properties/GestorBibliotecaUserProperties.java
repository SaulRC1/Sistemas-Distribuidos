package gestor.biblioteca.client.properties;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class GestorBibliotecaUserProperties 
{
    private int adminId = -1000;
    
    private static final GestorBibliotecaUserProperties instance = new GestorBibliotecaUserProperties();
    
    private GestorBibliotecaUserProperties()
    {
        
    }

    public int getAdminId()
    {
        return adminId;
    }

    public void setAdminId(int adminId)
    {
        this.adminId = adminId;
    }
    
    public static GestorBibliotecaUserProperties getInstance()
    {
        return instance;
    }
}
