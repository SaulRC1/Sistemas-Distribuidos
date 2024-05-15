package gestor.biblioteca.client.connection;

import gestor.biblioteca.service.GestorBibliotecaIntf;
import java.net.MalformedURLException;
import java.rmi.*;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class GestorBibliotecaServerConnection
{
    private String host;
    private String port;
    private String resourceName;
    
    private GestorBibliotecaIntf gestorBiblioteca;

    public GestorBibliotecaServerConnection(String host, String port, String resourceName)
    {
        this.host = host;
        this.port = port;
        this.resourceName = resourceName;
    }
    
    public GestorBibliotecaServerConnection()
    {
        
    }

    public String getHost()
    {
        return host;
    }

    public void setHost(String host)
    {
        this.host = host;
    }

    public String getPort()
    {
        return port;
    }

    public void setPuerto(String port)
    {
        this.port = port;
    }

    public String getResourceName()
    {
        return resourceName;
    }

    public void setResourceName(String resourceName)
    {
        this.resourceName = resourceName;
    }

    public GestorBibliotecaIntf getGestorBiblioteca()
    {
        return gestorBiblioteca;
    }
    
    public void connectToGestorBibliotecaServer() throws NotBoundException, MalformedURLException, RemoteException
    {
        this.gestorBiblioteca = (GestorBibliotecaIntf) Naming.lookup("rmi://" + this.host +
                ":" + this.port + "/" + this.resourceName);
    }
    
}
