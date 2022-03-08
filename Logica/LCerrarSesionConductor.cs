using System;
using Utilitarios;
using Data;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Logica
{
    public class LCerrarSesionConductor
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

        public async Task cerrarSesion(int idConductor)//S
        {
           await new DaoSeguridadConductor().cerrarAcceso(idConductor);
        }

        public async Task insertarAcceso(AccesoConductor acceso)
        {
            await new DaoSeguridadConductor().insertarAcceso(acceso);
        }
    }
}
