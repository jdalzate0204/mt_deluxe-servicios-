using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Data;
using Utilitarios;

namespace Logica
{
    public class LCerrarSesionCliente
    {
        public String mac()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            String sMacAddress = string.Empty;

            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty) // only return MAC Address from first card    
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();

                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public String ip()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }

        public async Task cerrarSesion(int idCliente) 
        {
            await new DaoSeguridadCliente().cerrarAcceso(idCliente);
        }

        public async Task insertarAcceso(AccesoCliente acceso)
        {
             await   new DaoSeguridadCliente().insertarAcceso(acceso);
        }
    }
}
