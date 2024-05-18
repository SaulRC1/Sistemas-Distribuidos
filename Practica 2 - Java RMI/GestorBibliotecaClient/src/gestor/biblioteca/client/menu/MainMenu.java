package gestor.biblioteca.client.menu;

import gestor.biblioteca.client.properties.GestorBibliotecaUserProperties;
import gestor.biblioteca.service.GestorBibliotecaIntf;
import java.rmi.RemoteException;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class MainMenu
{
    public static final String MAIN_MENU_TITLE = "GESTOR BIBLIOTECARIO 2.0 (M.PRINCIPAL)";
    public static final String OPTION_0_TITLE = "0.- Salir";
    public static final String OPTION_1_TITLE = "1.- M. Administración";
    public static final String OPTION_2_TITLE = "2.- Consulta de libros";
    public static final String OPTION_3_TITLE = "3.- Préstamo de libros";
    public static final String OPTION_4_TITLE = "4.- Devolución de libros";
    
    private GestorBibliotecaIntf gestorBiblioteca;
    private Scanner scanner;

    public MainMenu(GestorBibliotecaIntf gestorBiblioteca, Scanner scanner)
    {
        this.gestorBiblioteca = gestorBiblioteca;
        this.scanner = scanner;
    }
    
    public void showMainMenu()
    {
        System.out.println(MAIN_MENU_TITLE);
        System.out.println("*******************************");
        System.out.println(OPTION_1_TITLE);
        System.out.println(OPTION_2_TITLE);
        System.out.println(OPTION_3_TITLE);
        System.out.println(OPTION_4_TITLE);
        System.out.println(OPTION_0_TITLE);
    }
    
    public void executeOption0()
    {
        System.exit(0);
    }
    
    public void executeOption1()
    {
        try
        {
            //Clean buffer
            scanner.nextLine();
            
            String password = "";
            
            System.out.println("Por favor inserte la contraseña de administración:");
            password = scanner.nextLine();
            
            int connectionResult = gestorBiblioteca.Conexion(password);
            
            switch (connectionResult)
            {
                case -1:
                    System.out.println("ERROR: Ya hay un usuario identificado como administrador");
                    break;
                case -2:
                    System.out.println("ERROR: Contraseña errónea");
                default:
                    GestorBibliotecaUserProperties.getInstance().setAdminId(connectionResult);
                    
                    AdministrationMenu adminMenu = new AdministrationMenu(gestorBiblioteca, scanner);
                    
                    int option = -1;
                    
                    do
                    {
                        adminMenu.show();
                        
                        System.out.println("Selecciona una opción:");
                        option = scanner.nextInt();
                        
                        adminMenu.executeOption(option);
                        
                    } while (option != 0);
            }
        } catch (RemoteException ex)
        {
            System.out.println("Error de conexión: " + ex.getMessage());
        }
    }
    
    public void executeOption2()
    {
        
    }
    
    public void executeOption3()
    {
        
    }
    
    public void executeOption4()
    {
        
    }
    
    public void executeOption(int optionNumber)
    {
        switch (optionNumber)
        {
            case 0:
                executeOption0();
                break;
            case 1:
                executeOption1();
                break;
            case 2:
                executeOption2();
                break;
            case 3:
                executeOption3();
                break;
            case 4:
                executeOption4();
                break;
            default:
                System.out.println("opción incorrecta");
        }
    }
}
