package gestor.biblioteca.client.menu;

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
}
