using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Utilitarios;
using Utilitarios.Entrada;

namespace Data
{
    public class DaoConductor
    {
        //Validacion de login conductor (S)
        public async Task<Conductor> login(LoginRequest conductor)
        {
            using (var db = new MapeoConductor())
            {
                Conductor conductorr = await db.conduc.Where(x => x.Usuario.ToUpper().Equals(conductor.Usuario.ToUpper()) && x.Contrasena.Equals(conductor.Contrasena)).
                    FirstOrDefaultAsync();
                return conductorr;
            }
        }

        public Conductor mostrarDatosLogin(string usuario)
        {
            return new MapeoConductor().conduc.Where(x => x.Usuario.Equals(usuario)).First();
        }

        //Valida existencia de usuario para generar token de recuperacion
        public Conductor getloginByUsuariocon(string usuario)
        {
            return new MapeoConductor().conduc.Where(x => x.Usuario.ToUpper().Equals(usuario.ToUpper())).FirstOrDefault();
        }

        //Validacion existencia usuario para registro(S)
        public Conductor validarExistencia(RegistroConductorRequest conductorER)
        {
            Conductor conductorE = new Conductor();
            conductorE.Usuario = conductorER.Usuario;

            return new MapeoConductor().conduc.Where(x => x.Usuario.Equals(conductorE.Usuario)).FirstOrDefault();
        }

        //Validacion existencia correo para registro(S)
        public Conductor validarExistenciaCorreo(RegistroConductorRequest conductorER)
        {
            Conductor conductorE = new Conductor();
            conductorE.Email = conductorER.Email;
            return new MapeoConductor().conduc.Where(x => x.Email.Equals(conductorE.Email)).FirstOrDefault();
        }

        //Inserta registro conductor (S)
        public async Task inserConductor(RegistroConductorRequest conductorR)
        {
            using (var db = new MapeoConductor())
            {
                Conductor conductor = new Conductor();
                conductor.Apellido = conductorR.Apellido;
                conductor.Cedula = conductorR.Cedula;
                conductor.Celular = conductorR.Celular;
                conductor.Contrasena = conductorR.Contrasena;
                conductor.Email = conductorR.Email;
                conductor.FechaDeNacimiento = conductorR.FechaDeNacimiento;
                conductor.Nombre = conductorR.Nombre;
                conductor.Placa = conductorR.Placa;
                conductor.Usuario = conductorR.Usuario;
                conductor.Modificado = "motodeluxe";
                conductor.Sesion = "espera";
                conductor.IdEstado = 3;
                conductor.Rol = 2;
                db.conduc.Add(conductor);
                await db.SaveChangesAsync();
            }
        }

        //Eliminacion de cuenta (S)
        public async Task eliminarConductor(Conductor conductor)
        {
            using (var db = new MapeoConductor())
            {
                Conductor conductorAnterior = db.conduc.Where(x => x.IdConductor == conductor.IdConductor).FirstOrDefault();
                conductorAnterior.Sesion = "inactivo";

                db.conduc.Attach(conductorAnterior);

                var entry = db.Entry(conductorAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Elimina token al eliminar cuenta y cerrar sesion (S)
        public async Task eliminarToken(ConductorTokenLogin token)
        {
            using (var db = new MapeoConductor())
            {
                ConductorTokenLogin tokenC = db.tokenLogin.Where(x => x.IdConductor == token.IdConductor).FirstOrDefault();
                db.tokenLogin.Remove(tokenC);
                await db.SaveChangesAsync();
            }
        }

        //Muestra registro para modificar
        public async Task<Conductor> mostrarRegistro(int idConductor)
        {
            return await new MapeoConductor().conduc.Where(x => x.IdConductor == idConductor).FirstAsync();
        }

        //Modifica registro
        public async Task modificarConductor(Conductor conductor)
        {
            using (var db = new MapeoConductor())
            {
                Conductor conductorAnterior = db.conduc.Where(x => x.IdConductor == conductor.IdConductor).FirstOrDefault();
                conductorAnterior.Nombre = conductor.Nombre;
                conductorAnterior.Apellido = conductor.Apellido;
                conductorAnterior.FechaDeNacimiento = conductor.FechaDeNacimiento;
                conductorAnterior.Email = conductor.Email;
                conductorAnterior.Placa = conductor.Placa;
                conductorAnterior.Usuario = conductor.Usuario;
                conductorAnterior.Contrasena = conductor.Contrasena;

                db.conduc.Attach(conductorAnterior);

                var entry = db.Entry(conductorAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Lista Estado (S)
        public List<Estado> estado()
        {
            List<Estado> lista = new MapeoConductor().estado.ToList();
            Estado state = new Estado();
            state.Id = 0;
            state.Disponibilidad = "-- Seleccione --";
            lista.Add(state);
            return lista.OrderBy(x => x.Id).ToList();
        }

        //Modificar Estado (S)
        public async Task estadoConductor(Conductor conductor)
        {
            using (var db = new MapeoConductor())
            {
                Conductor estadoAnterior = db.conduc.Where(x => x.IdConductor == conductor.IdConductor).First();
                estadoAnterior.IdEstado = conductor.IdEstado;

                db.conduc.Attach(estadoAnterior);

                var entry = db.Entry(estadoAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Muestra solicitudes de carreras (S)
        public async Task<List<Notificacion>> mostrarHistorial(int idEstado)
        {
            using (var db = new MapeoCliente())
            {
                var data = await (from n in db.notificacion
                                  join cl in db.client on n.IdCliente equals cl.IdCliente
                                  join d in db.destino on n.IdDestino equals d.Id
                                  join u in db.destino on n.IdUbicacion equals u.Id
                                  join p in db.pago on n.Pago equals p.Id
                                  orderby n.FechaCarrera
                                  select new
                                  {
                                      n,
                                      cl.Nombrecl,
                                      d.LugarDestino,
                                      u.LugarUbicacion,
                                      p.Descripcion
                                  }).ToListAsync();
                return data.Select(m => new Notificacion
                {
                    Id = m.n.Id,
                    IdCliente = m.n.IdCliente,
                    IdDestino = m.n.IdDestino,
                    IdUbicacion = m.n.IdUbicacion,
                    Tarifa = m.n.Tarifa,
                    FechaCarrera = m.n.FechaCarrera,
                    Estado = m.n.Estado,
                    IdConductor = m.n.IdConductor,
                    Conductor = m.n.Conductor,
                    ComentarioDeConductor = m.n.ComentarioDeConductor,
                    FechaFinCarrera = m.n.FechaFinCarrera,
                    NombreCl = m.Nombrecl,
                    Destino = m.LugarDestino,
                    Ubicacion = m.LugarUbicacion,
                    MetodoPago = m.Descripcion
                }).Where(x => x.Estado.Contains("Pendiente") && idEstado != 3).OrderBy(x => x.FechaCarrera).ToList();
            }
        }

        //Aceptar el servicio (S)
        public async Task aceptarServicio(Notificacion notificacion)
        {
            using (var db = new MapeoConductor())
            {
                Notificacion notificacionAnterior = db.notificacion.Where(x => x.Id == notificacion.Id).FirstOrDefault();
                notificacionAnterior.Estado = notificacion.Estado;
                notificacionAnterior.Conductor = notificacion.Conductor;
                notificacionAnterior.IdConductor = notificacion.IdConductor;

                db.notificacion.Attach(notificacionAnterior);

                var entry = db.Entry(notificacionAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Para capturar email cliente (S)
        public Cliente buscarEmail(double idCliente)
        {
            return new MapeoCliente().client.Where(x => x.IdCliente == idCliente).First();
        }

        //Select Id Notificacion (S)
        public Notificacion buscarId(int notificacionId)
        {
            return new MapeoConductor().notificacion.Where(x => x.Id == notificacionId).FirstOrDefault();
        }

        //Trae registro para capturar el idConductor en aceptar servicio (S)
        public Conductor mostrarDatos(int idCondutor)
        {
            return new MapeoConductor().conduc.Where(x => x.IdConductor == idCondutor).First();
        }

        //Trae el conductor para aceptar solicitud
        public Conductor traerConductor(string usuario)
        {
            return new MapeoConductor().conduc.Where(x => x.Usuario.Equals(usuario)).First();
        }

        //Muestra las carreras (filtro)
        public async Task<List<Notificacion>> filtrarCarrera(DateTime? fechaInicio, int idConductor)
        {
            using (var db = new MapeoCliente())
            {
                var filtro = await (from n in db.notificacion
                                    join cl in db.client on n.IdCliente equals cl.IdCliente
                                    join d in db.destino on n.IdDestino equals d.Id
                                    join u in db.destino on n.IdUbicacion equals u.Id
                                    join p in db.pago on n.Pago equals p.Id
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
                    IdDestino = m.n.IdDestino,
                    IdUbicacion = m.n.IdUbicacion,
                    Tarifa = m.n.Tarifa,
                    FechaCarrera = m.n.FechaCarrera,
                    Estado = m.n.Estado,
                    IdConductor = m.n.IdConductor,
                    Conductor = m.n.Conductor,
                    ComentarioDeConductor = m.n.ComentarioDeConductor,
                    FechaFinCarrera = m.n.FechaFinCarrera,
                    Conversacion = m.n.Conversacion,
                    NombreCl = m.Nombrecl,
                    Destino = m.LugarDestino,
                    Ubicacion = m.LugarUbicacion
                }).Where(x => x.Estado.Contains("Aceptado") && x.IdConductor == idConductor).ToList();

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
            using (var db = new MapeoConductor())
            {
                Notificacion notificacionAnterior = db.notificacion.Where(x => x.Id == notificacion.Id).FirstOrDefault();
                notificacionAnterior.ComentarioDeConductor = notificacion.ComentarioDeConductor;
                notificacionAnterior.FechaFinCarrera = DateTime.Now;

                db.notificacion.Attach(notificacionAnterior);

                var entry = db.Entry(notificacionAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Conversacion (S)
        public async Task coversar(Notificacion notificacion)
        {
            using (var db = new MapeoConductor())
            {
                Notificacion notificacionAnterior = db.notificacion.Where(x => x.Id == notificacion.Id).FirstOrDefault();
                notificacionAnterior.Conversacion = notificacion.Conversacion;

                db.notificacion.Attach(notificacionAnterior);

                var entry = db.Entry(notificacionAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Calcular ganancias
        public double ganancias(Notificacion notificacionT)//S
        {
           return new MapeoConductor().notificacion.Where(x => x.IdConductor == notificacionT.IdConductor && x.Estado.Contains("Aceptado")).Sum(x => x.Tarifa);

        }

        //Generar reporte (historial carreras) (S)
        public List<Notificacion> reporteHistorial(Notificacion notificacio, int idCo)
        {
            using (var db = new MapeoCliente())
            {
                return (from n in db.notificacion
                        join cl in db.client on n.IdCliente equals cl.IdCliente
                        join d in db.destino on n.IdDestino equals d.Id
                        join u in db.destino on n.IdUbicacion equals u.Id
                        join p in db.pago on n.Pago equals p.Id
                        orderby n.FechaCarrera
                        select new
                        {
                            n,
                            cl.Nombrecl,
                            d.LugarDestino,
                            u.LugarUbicacion
                        }).ToList().Select(m => new Notificacion
                        {
                            Id = m.n.Id,
                            IdCliente = m.n.IdCliente,
                            IdDestino = m.n.IdDestino,
                            IdUbicacion = m.n.IdUbicacion,
                            Tarifa = m.n.Tarifa,
                            FechaCarrera = m.n.FechaCarrera,
                            Estado = m.n.Estado,
                            IdConductor = m.n.IdConductor,
                            Conductor = m.n.Conductor,
                            ComentarioDeConductor = m.n.ComentarioDeConductor,
                            FechaFinCarrera = m.n.FechaFinCarrera,
                            Conversacion = m.n.Conversacion,
                            NombreCl = m.Nombrecl,
                            Destino = m.LugarDestino,
                            Ubicacion = m.LugarUbicacion
                        }).Where(x => x.Estado.Contains("Aceptado") && x.IdConductor == idCo).ToList();
            }
        }
    }
}