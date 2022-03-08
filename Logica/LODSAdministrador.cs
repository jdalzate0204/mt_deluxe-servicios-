using System.Collections.Generic;
using Data;
using Utilitarios;

namespace Logica
{
    public class LODSAdministrador
    {
        public List<Conductor> mostrarConductorAceptar()//S
        {
            return new DaoAdministrador().mostrarConductorAceptar();
        }

        public List<Conductor> mostrarConductores() //S
        {
            return new DaoAdministrador().mostrarConductores();
        }

        public List<Cliente> mostrarClienteAceptar()//S 
        {
            return new DaoAdministrador().mostrarClienteAceptar();
        }

        public List<Cliente> mostrarClientes() //S
        {
            return new DaoAdministrador().mostrarClientes();
        }

        public List<Notificacion> conductoresPago()//S
        {
            return new DaoAdministrador().conductoresPago();
        }

        public List<Notificacion> mostrarCarreraConductor()//S
        {
            return new DaoAdministrador().mostrarCarreraConductor();
        }

        public List<Notificacion> mostrarServiciosCliente() //S
        {
            return new DaoAdministrador().mostrarServiciosCliente();
        }
    }
}
