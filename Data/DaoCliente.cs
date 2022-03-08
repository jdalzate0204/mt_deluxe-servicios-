using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Utilitarios;
using Utilitarios.Entrada;

namespace Data
{
    public class DaoCliente
    {
        //Validacion de login cliente (S)
        public async Task<Cliente> login(LoginRequest cliente)
        {
            using (var db = new MapeoCliente())
            {
                Cliente clientee = await db.client.Where(x => x.Usuario.ToUpper().Equals(cliente.Usuario.ToUpper()) && x.Contrasena.Equals(cliente.Contrasena))
                    .FirstOrDefaultAsync();
                return clientee;
            }
        }

        //Valida existencia de usuario para generar token de recuperacion
        public Cliente getloginByUsuario(string usuario)
        {
            
            return new MapeoCliente().client.Where(x => x.Usuario.ToUpper().Equals(usuario.ToUpper())).FirstOrDefault();
        }

        //Validacion existencia usuario para registro (S)
        public Cliente validarExistencia(RegistroClienteRequest clienteER)
        {
            Cliente clienteE = new Cliente();
            clienteE.Usuario = clienteER.Usuario;
            return new MapeoCliente().client.Where(x => x.Usuario.Equals(clienteE.Usuario)).FirstOrDefault();
        }

        //Validar existencia correo para registro (S)
        public Cliente validarExistenciaCorreos(RegistroClienteRequest clienteER)
        {
            Cliente clienteE = new Cliente();
            clienteE.Email = clienteER.Email;
            return new MapeoCliente().client.Where(x => x.Email.Equals(clienteE.Email)).FirstOrDefault();
        }

        //Inserta registro cliente (S)
        public async Task inserCliente(RegistroClienteRequest clienteR)
        {
            using (var db = new MapeoCliente())
            {
                Cliente cliente = new Cliente();
                cliente.Nombrecl = clienteR.Nombrecl;
                cliente.Apellido = clienteR.Apellido;
                cliente.FechaDeNacimiento = clienteR.FechaDeNacimiento;
                cliente.Email = clienteR.Email;
                cliente.Usuario = clienteR.Usuario;
                cliente.Contrasena = clienteR.Contrasena;
                cliente.Modificado = "mototaxideluxe";
                cliente.Sesion = "activo";
                cliente.Rol = 1;
                db.client.Add(cliente);
                await db.SaveChangesAsync();
            }
        }

        //Eliminacion de cuenta (S)
        public async Task eliminarCliente(Cliente cliente)
        {
            using (var db = new MapeoCliente())
            {
                Cliente clienteAnterior = db.client.Where(x => x.IdCliente == cliente.IdCliente).FirstOrDefault();
                clienteAnterior.Sesion = "inactivo";

                db.client.Attach(clienteAnterior);

                var entry = db.Entry(clienteAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Eliminacion de registro (S)
        public async Task eliminarRegistro(Cliente cliente)
        {
            using (var db = new MapeoCliente())
            {
                Cliente clienteAnterior = db.client.Where(x => x.IdCliente == cliente.IdCliente).FirstOrDefault();
                clienteAnterior.Sesion = "inactivo";

                db.client.Attach(clienteAnterior);

                var entry = db.Entry(clienteAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Elimina token al eliminar cuenta y cerrar sesion (S)
        public async Task eliminarToken(ClienteTokenLogin token)
        {
            using (var db = new MapeoCliente())
            {
                ClienteTokenLogin tokenC = db.tokenLogin.Where(x => x.IdCliente == token.IdCliente).FirstOrDefault();
                db.tokenLogin.Remove(tokenC);
                await db.SaveChangesAsync();
            }
        }

        //Muestra registro para modificar (S)
        public async Task<Cliente> mostrarRegistro(int idCliente)
        {
            return await new MapeoCliente().client.Where(x => x.IdCliente == idCliente).FirstAsync();
        }

        //Modifica registro (S)
        public async Task modificarCliente(Cliente cliente)
        {
            using (var db = new MapeoCliente())
            {
                Cliente clienteAnterior = db.client.Where(x => x.IdCliente == cliente.IdCliente).FirstOrDefault();
                clienteAnterior.Nombrecl = cliente.Nombrecl;
                clienteAnterior.Apellido = cliente.Apellido;
                clienteAnterior.FechaDeNacimiento = cliente.FechaDeNacimiento;
                clienteAnterior.Email = cliente.Email;
                clienteAnterior.Usuario = cliente.Usuario;
                clienteAnterior.Contrasena = cliente.Contrasena;

                db.client.Attach(clienteAnterior);

                var entry = db.Entry(clienteAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Muestra conductores disponibles (S)
        public List<Conductor> conductoresDisponibles()
        {
            using (var db = new MapeoConductor())
            {
                return db.conduc.Select(x => new
                {
                    x.IdConductor,
                    x.Nombre,
                    x.Apellido,
                    x.Sesion,
                    x.IdEstado
                }).ToList().Select(x => new Conductor()
                {
                    IdConductor = x.IdConductor,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Sesion = x.Sesion,
                    IdEstado = x.IdEstado
                }).Where(x => x.IdEstado == 1 && x.Sesion.Equals("activo")).OrderBy(x => x.IdConductor).ToList();
            }
        }

        //Lista Destino (S)
        public List<Destino> destino()
        {
            List<Destino> lista = new MapeoCliente().destino.ToList();
            Destino destino = new Destino();
            destino.Id = 0;
            destino.LugarDestino = "-- Seleccione --";
            lista.Add(destino);
            return lista.OrderBy(x => x.Id).ToList();
        }

        //Lista Ubicacion (S)
        public List<Destino> ubicacion()
        {
            List<Destino> lista = new MapeoCliente().destino.ToList();
            Destino destino = new Destino();
            destino.Id = 0;
            destino.LugarUbicacion = "-- Seleccione --";
            lista.Add(destino);
            return lista.OrderBy(x => x.Id).ToList();
        }

        //Lista Pago (S)
        public List<MPago> pago()
        {
            List<MPago> lista = new MapeoCliente().pago.ToList();
            MPago state = new MPago();
            state.Id = 0;
            state.Descripcion = "-- Seleccione --";
            lista.Add(state);
            return lista.OrderBy(x => x.Id).ToList();
        }

        //Inserta el pedido de servicio (S)
        public async Task inserServicio(ServicioClienteRequest servicio, int idCliente)
        {
            using (var db = new MapeoCliente())
            {

                Notificacion notificacion = new Notificacion();
                notificacion.IdDestino = servicio.idDestino;
                notificacion.IdUbicacion = servicio.idUbicacion;
                notificacion.DescripcionServicio = servicio.descripcionServicio;
                notificacion.Pago = servicio.pago;
                notificacion.IdCliente = idCliente;
                notificacion.Tarifa = servicio.tarifa;
                notificacion.Kilometro = servicio.kilometros;
                notificacion.FechaCarrera = DateTime.Now.Date;
                notificacion.Estado = "Pendiente";

                db.notificacion.Add(notificacion);
                await db.SaveChangesAsync();
            }
        }

        //Trae registro para capturar el idCliente en solicitud servicio (S)
        public Cliente mostrarDatos(string usuario)
        {
            return new MapeoCliente().client.Where(x => x.Usuario.Equals(usuario)).First();
        }

        //Trae registro para capturar el idCliente en solicitud servicio (S)
        public Notificacion mostrarNotificacion(int idCliente)
        {
          return  new MapeoCliente().notificacion.Where(x => x.IdCliente == idCliente && x.Estado.Equals("Pendiente")).ToList().Last();

        }

        //Generar reporte (factura de pedido de servicio)(S)
        public Notificacion generarFactura(Notificacion notificacion, int idCliente)
        {
            using (var db = new MapeoCliente())
            {
                return (from n in db.notificacion
                        join cl in db.client on n.IdCliente equals cl.IdCliente
                        join d in db.destino on n.IdDestino equals d.Id
                        join u in db.destino on n.IdUbicacion equals u.Id
                        join p in db.pago on n.Pago equals p.Id
                        orderby n.Id
                        select new
                        {
                            n,
                            cl.Nombrecl,
                            d.LugarDestino,
                            u.LugarUbicacion,
                            p.Descripcion
                        }).ToList().Select(m => new Notificacion
                        {
                            Id = m.n.Id,
                            IdCliente = m.n.IdCliente,
                            IdDestino = m.n.IdDestino,
                            IdUbicacion = m.n.IdUbicacion,
                            Tarifa = m.n.Tarifa,
                            FechaCarrera = m.n.FechaCarrera,
                            Pago = m.n.Pago,
                            NombreCl = m.Nombrecl,
                            Destino = m.LugarDestino,
                            Ubicacion = m.LugarUbicacion,
                            MetodoPago = m.Descripcion
                        }).Where(x => x.IdCliente == idCliente).Last();
            }
        }

        //Muestra los servicios (filtro) (S)
        public async Task<List<Notificacion>> filtrarServicios(DateTime? fechaInicio, int idCliente)
        {
            using (var db = new MapeoCliente())
            {
                var filtro = await (from n in db.notificacion
                                    join cl in db.client on n.IdCliente equals cl.IdCliente
                                    join d in db.destino on n.IdDestino equals d.Id
                                    join u in db.destino on n.IdUbicacion equals u.Id
                                    orderby n.FechaCarrera
                                    select new
                                    {
                                        n,
                                        cl.Nombrecl,
                                        d.LugarDestino,
                                        u.LugarUbicacion
                                    }).ToListAsync();
                List<Notificacion> fil = filtro.Select(m => new Notificacion
                {
                    Id = m.n.Id,
                    IdCliente = m.n.IdCliente,
                    IdConductor = m.n.IdConductor,
                    IdDestino = m.n.IdDestino,
                    IdUbicacion = m.n.IdUbicacion,
                    NombreCl = m.Nombrecl,
                    Conductor = m.n.Conductor,
                    Destino = m.LugarDestino,
                    Ubicacion = m.LugarUbicacion,
                    Tarifa = m.n.Tarifa,
                    FechaCarrera = m.n.FechaCarrera,
                    ComentarioDeCliente = m.n.ComentarioDeCliente,
                    Conversacion = m.n.Conversacion,
                    Estado = m.n.Estado,
                    FechaFinCarrera = m.n.FechaFinCarrera
                }).Where(x => x.Estado.Contains("Aceptado") && x.IdCliente == idCliente).ToList();

                if (fechaInicio != null)
                {
                    fil = fil.Where(x => x.FechaCarrera == fechaInicio).ToList();
                    return fil;
                }
                return fil;
            }
        }

        //Añadir comentario en tabla historial (S)
        public async Task comentar(Notificacion notificacion)
        {
            using (var db = new MapeoCliente())
            {
                Notificacion notificacionAnterior = db.notificacion.Where(x => x.Id == notificacion.Id).FirstOrDefault();
                notificacionAnterior.ComentarioDeCliente = notificacion.ComentarioDeCliente;

                db.notificacion.Attach(notificacionAnterior);

                var entry = db.Entry(notificacionAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Conversacion (S)
        public async Task coversar(Notificacion notificacion)
        {
            using (var db = new MapeoCliente())
            {
                Notificacion notificacionAnterior = db.notificacion.Where(x => x.Id == notificacion.Id).FirstOrDefault();
                notificacionAnterior.Conversacion = notificacion.Conversacion;

                db.notificacion.Attach(notificacionAnterior);

                var entry = db.Entry(notificacionAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Trae el usuario (S)
        public Cliente mostrarDatosLogin(string  usuario)
        {
            return new MapeoCliente().client.Where(x => x.Usuario.Equals(usuario)).First();
        }

        //Trae el registro (S)
        public List<Cliente> mostrarRegistro()
        {
            return new MapeoCliente().client.Where(x => x.Sesion.Equals("activo")).ToList();
        }

        ////Prueba
        //public async Task<List<Cliente>> obtenerClientes()
        //{
        //    using (var db = new MapeoCliente())
        //    {
        //        return await db.client.OrderBy(x => x.IdCliente).ToListAsync();
        //    }
        //}

        //public async Task<List<Cliente>> obtenerClientesByNombre(string nombre)
        //{
        //    using (var db = new MapeoCliente())
        //    {
        //        return await db.client.Where(x => x.Nombrecl.Contains(nombre)).ToListAsync();
        //    }
        //}
    }
}