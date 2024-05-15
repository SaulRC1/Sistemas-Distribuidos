package gestor.biblioteca.client;

import gestor.biblioteca.client.connection.GestorBibliotecaServerConnection;
import gestor.biblioteca.client.menu.MainMenu;
import java.net.MalformedURLException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class GestorBibliotecaClient
{

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args)
    {
        GestorBibliotecaServerConnection gestorBibliotecaServerConnection = new GestorBibliotecaServerConnection();
        
        try
        {
            gestorBibliotecaServerConnection.connectToGestorBibliotecaServer();
            
            MainMenu mainMenu = new MainMenu();
        
            do
            {
              mainMenu.showMainMenu();  
            } while (true);
            
        } catch (NotBoundException ex)
        {
            Logger.getLogger(GestorBibliotecaClient.class.getName()).log(Level.SEVERE, ex.getMessage(), ex);
        } catch (MalformedURLException ex)
        {
            Logger.getLogger(GestorBibliotecaClient.class.getName()).log(Level.SEVERE, ex.getMessage(), ex);
        } catch (RemoteException ex)
        {
            Logger.getLogger(GestorBibliotecaClient.class.getName()).log(Level.SEVERE, ex.getMessage(), ex);
        }
    }

}
