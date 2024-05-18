package gestor.biblioteca.client;

import gestor.biblioteca.client.connection.GestorBibliotecaServerConnection;
import gestor.biblioteca.client.menu.MainMenu;
import java.net.MalformedURLException;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.Scanner;
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
        Scanner scanner = new Scanner(System.in);
        
        String host;
        int port;
        String resourceName;
        
        System.out.println("Introduzca el host al que quiere conectarse: ");
        host = scanner.nextLine();
        
        System.out.println("Introduzca el puerto del host: ");
        port = scanner.nextInt();
        
        //Clean scanner input buffer
        scanner.nextLine();
        
        System.out.println("Introduzca el nombre del recurso al que quiere conectarse: ");
        resourceName = scanner.nextLine();
        
        GestorBibliotecaServerConnection gestorBibliotecaServerConnection = 
                new GestorBibliotecaServerConnection(host, port, resourceName);
        
        try
        {
            gestorBibliotecaServerConnection.connectToGestorBibliotecaServer();
            
            MainMenu mainMenu = new MainMenu(gestorBibliotecaServerConnection.getGestorBiblioteca(), scanner);
        
            int option = -1;
            
            do
            {   
                mainMenu.showMainMenu();
                
                System.out.println("Selecciona una opción:");
                option = scanner.nextInt();
                
                mainMenu.executeOption(option);
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
        finally
        {
            scanner.close();
        }
    }

}
