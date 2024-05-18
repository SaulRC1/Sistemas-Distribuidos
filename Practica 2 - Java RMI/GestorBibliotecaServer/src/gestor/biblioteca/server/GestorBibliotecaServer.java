package gestor.biblioteca.server;

import gestor.biblioteca.server.remote.GestorBiblioteca;
import gestor.biblioteca.service.GestorBibliotecaIntf;
import java.net.MalformedURLException;
import java.rmi.*;
import java.rmi.registry.*;
import java.rmi.server.*;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class GestorBibliotecaServer
{

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        
        System.out.println("Introduce puerto para la comunicación: ");
        
        int port = scanner.nextInt();
        
        try
        {
            Registry registry = LocateRegistry.createRegistry(port);
            
            GestorBiblioteca gestorBiblioteca = new GestorBiblioteca();
            
            GestorBibliotecaIntf stub = (GestorBibliotecaIntf) UnicastRemoteObject
                    .exportObject(gestorBiblioteca, port);
            
            registry = LocateRegistry.getRegistry(port);
            registry.bind("GestorBiblioteca", stub);
            
            Logger.getLogger(GestorBibliotecaServer.class.getName()).log(Level.INFO, "Servidor iniciado correctamente");
            
        } catch (RemoteException ex)
        {
            Logger.getLogger(GestorBibliotecaServer.class.getName()).log(Level.SEVERE, null, ex);
        } catch (AlreadyBoundException ex)
        {
            Logger.getLogger(GestorBibliotecaServer.class.getName()).log(Level.SEVERE, null, ex);
        }
        finally
        {
            scanner.close();
        }
       
    }

}
