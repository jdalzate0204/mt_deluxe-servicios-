using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Utilitarios;

namespace Data
{
    public class DaoAdministrador
    {
        //Validacion de login administrador (S)
        public async Task<Admin> login(LoginRequest administrador)
        {
            using (var db = new MapeoAdministrador())
            {
                Admin admin = await db.administrador.Where(x => x.Usuario.ToUpper().Equals(administrador.Usuario.ToUpper()) && 
                x.Contrasena.Equals(administrador.Contrasena)).FirstOrDefaultAsync();
                return admin;
            }
        }

        //Elimina token al cerrar sesion (S)
        public async Task eliminarToken(AdministradorTokenLogin token)
        {
            using (var db = new MapeoAdministrador())
            {
                AdministradorTokenLogin tokenC = db.tokenLogin.Where(x => x.IdAdmin == token.IdAdmin).FirstOrDefault();
                db.tokenLogin.Remove(tokenC);
                await db.SaveChangesAsync();
            }
        }

        //Para Linea de Profundización 1
        //Muestra registro al loguear
        public Admin mostrarDatosLogin(string usuario)
        {
            return new MapeoAdministrador().administrador.Where(x => x.Usuario.Equals(usuario)).First();
        }


        //Muestra para aceptar conductor
        public List<Conductor> mostrarConductorAceptar()//S
        {
            using (var db = new MapeoConductor())
            {
                return db.conduc.Select(x => new
                {
                    x.IdConductor,
                    x.Nombre,
                    x.Apellido,
                    x.Usuario,
                    x.Sesion
                }).ToList().Select(x => new Conductor()
                {
                    IdConductor = x.IdConductor,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Usuario = x.Usuario,
                    Sesion = x.Sesion
                }).Where(x => x.Sesion.Contains("espera") || x.Sesion.Contains("sancionado")).ToList();
            }
        }

        //Para captura email conductor
        public Conductor buscarEmail(int idConductor)//S
        {
            return new MapeoConductor().conduc.Where(x => x.IdConductor == idConductor).First();
        }

        //Modifica la sesion de conductor al aceptar registro
        public void sesionConductor(Conductor conductor)
        {
            using (var db = new MapeoConductor())
            {
                Conductor estadoAnterior = db.conduc.Where(x => x.IdConductor == conductor.IdConductor).First();
                estadoAnterior.Sesion = conductor.Sesion;

                db.conduc.Attach(estadoAnterior);

                var entry = db.Entry(estadoAnterior);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Muestra registro de conductores (S)
        public List<Conductor> mostrarConductores()
        {
            using (var db = new MapeoConductor())
            {
                return db.conduc.Select(x => new
                {
                    x.IdConductor,
                    x.Nombre,
                    x.Apellido,
                    x.Cedula,
                    x.FechaDeNacimiento,
                    x.Email,
                    x.Placa,
                    x.Celular,
                    x.Usuario,
                    x.Sesion
                }).ToList().Select(x => new Conductor()
                {
                    IdConductor = x.IdConductor,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Cedula = x.Cedula,
                    FechaDeNacimiento = x.FechaDeNacimiento,
                    Email = x.Email,
                    Placa = x.Placa,
                    Celular = x.Celular,
                    Usuario = x.Usuario,
                    Sesion = x.Sesion
                }).OrderBy(x => x.IdConductor).ToList();
            }
        }

        //Muestra para aceptar cliente
        public List<Cliente> mostrarClienteAceptar()//S
        {
            using (var db = new MapeoCliente())
            {
                return db.client.Select(x => new
                {
                    x.IdCliente,
                    x.Nombrecl,
                    x.Apellido,
                    x.Usuario,
                    x.Sesion
                }).ToList().Select(x => new Cliente()
                {
                    IdCliente = x.IdCliente,
                    Nombrecl = x.Nombrecl,
                    Apellido = x.Apellido,
                    Usuario = x.Usuario,
                    Sesion = x.Sesion
                }).Where(x => x.Sesion.Contains("sancionado")).ToList();
            }
        }

        //Para captura email cliente
        public Cliente buscarEmailCl(double idCliente)//S
        {
            return new MapeoCliente().client.Where(x => x.IdCliente == idCliente).First();
        }

        //Modifica la sesion de cliente al aceptar registro
        public void sesionCliente(Cliente cliente)//S
        {
            using (var db = new MapeoCliente())
            {
                Cliente estadoAnterior = db.client.Where(x => x.IdCliente == cliente.IdCliente).First();
                estadoAnterior.Sesion = cliente.Sesion;

                db.client.Attach(estadoAnterior);

                var entry = db.Entry(estadoAnterior);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Muestra registro de clientes (S)
        public List<Cliente> mostrarClientes()
        {
            using (var db = new MapeoCliente())
            {
                return db.client.Select(x => new
                {
                    x.IdCliente,
                    x.Nombrecl,
                    x.Apellido,
                    x.FechaDeNacimiento,
                    x.Email,
                    x.Usuario,
                    x.Sesion
                }).ToList().Select(x => new Cliente()
                {
                    IdCliente = x.IdCliente,
                    Nombrecl = x.Nombrecl,
                    Apellido = x.Apellido,
                    FechaDeNacimiento = x.FechaDeNacimiento,
                    Email = x.Email,
                    Usuario = x.Usuario,
                    Sesion = x.Sesion
                }).OrderBy(x => x.IdCliente).ToList();
            }
        }

        //Lista de conductores para pago
        public List<Notificacion> conductoresPago()//S
        {
            using (var db = new MapeoConductor())
            {
                List<Notificacion> lista = (from n in db.notificacion
                                            join co in db.conduc on n.IdConductor equals co.IdConductor
                                            select new
                                            {
                                                n,
                                                co.Nombre
                                            }).ToList().Select(m => new Notificacion
                                            {
                                                Id = m.n.Id,
                                                IdConductor = m.n.IdConductor,
                                                NombreCo = m.Nombre
                                            }).OrderBy(x => x.IdConductor).ToList();

                var conductores = lista.GroupBy(x => x.IdConductor).Select(grp => grp.ToList());

                List<Notificacion> listaCo = new List<Notificacion>();

                foreach (var item in conductores)
                {
                    Notificacion notificacion = new Notificacion();
                    notificacion.ListaConductores = item;
                    notificacion.IdConductor = notificacion.ListaConductores.First().IdConductor;
                    notificacion.NombreCo = notificacion.ListaConductores.First().NombreCo;
                    listaCo.Add(notificacion);
                }
                return listaCo;
            }
        }

        //Generar reporte (desprendible conductor) (S)
        public Notificacion generarDesprendible(Notificacion notificacion)
        {
            using (var db = new MapeoConductor())
            {
                return (from n in db.notificacion
                        join co in db.conduc on n.IdConductor equals co.IdConductor
                        select new
                        {
                            n,
                            co.Nombre,
                            co.Apellido,
                            co.Cedula,
                            co.Placa
                        }).ToList().Select(m => new Notificacion
                        {
                            Id = m.n.Id,
                            IdConductor = m.n.IdConductor,
                            Tarifa = m.n.Tarifa,
                            NombreCo = m.Nombre,
                            ApellidoCo = m.Apellido,
                            Cedula = m.Cedula,
                            Placa = m.Placa
                        }).Where(x => x.IdConductor == notificacion.IdConductor).FirstOrDefault();
            }
        }

        //Calcular Ganancias (Reporte)
        public double ganancias(int idConductor)
        {
            return new MapeoConductor().notificacion.Where(x => x.IdConductor == idConductor).Sum(x => x.Tarifa);
        }

        //Muestra los comentarios de cliente a conductor
        public List<Notificacion> mostrarCarreraConductor()
        {
            using (var db = new MapeoConductor())
            {
                return (from n in db.notificacion
                        join co in db.conduc on n.IdConductor equals co.IdConductor
                        join cl in db.cliente on n.IdCliente equals cl.IdCliente
                        orderby n.FechaCarrera
                        select new
                        {
                            n,
                            co.Nombre,
                            co.Sesion,
                            cl.Nombrecl
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
                            ComentarioDeCliente = m.n.ComentarioDeCliente,
                            FechaFinCarrera = m.n.FechaFinCarrera,
                            NombreCo = m.Nombre,
                            Sesion = m.Sesion,
                            NombreCl=m.Nombrecl
                        }).Where(x => x.Estado.Contains("Aceptado") && x.ComentarioDeCliente != null).OrderBy(x => x.FechaCarrera).ToList();
            }
        }

        //Muestra los comentarios de conductor a clientes
        public List<Notificacion> mostrarServiciosCliente()
        {
            using (var db = new MapeoCliente())
            {
                return (from n in db.notificacion
                        join cl in db.client on n.IdCliente equals cl.IdCliente
                        join co in db.conduc on n.IdConductor equals co.IdConductor
                        orderby n.FechaCarrera
                        select new
                        {
                            n,
                            cl.Nombrecl,
                            cl.Sesion,
                            co.Nombre
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
                            NombreCl = m.Nombrecl,
                            Sesion = m.Sesion,
                            NombreCo=m.Nombre
                        }).Where(x => x.Estado.Contains("Aceptado") && x.ComentarioDeConductor != null).OrderBy(x => x.FechaCarrera).ToList();
            }
        }

        //Modifica la sesion del conductor al sancionar
        public void sancionConductor(Conductor conductor)//S
        {
            using (var db = new MapeoConductor())
            {
                Conductor sancion = db.conduc.Where(x => x.IdConductor == conductor.IdConductor).First();
                sancion.Sesion = conductor.Sesion;
                sancion.FechaSancion = conductor.FechaSancion;

                db.conduc.Attach(sancion);

                var entry = db.Entry(sancion);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Modifica la sesion del cliente al sancionar
        public void sancionCliente(Cliente cliente)
        {
            using (var db = new MapeoCliente())
            {
                Cliente sancion = db.client.Where(x => x.IdCliente == cliente.IdCliente).First();
                sancion.Sesion = cliente.Sesion;
                sancion.FechaSancion = cliente.FechaSancion;

                db.client.Attach(sancion);

                var entry = db.Entry(sancion);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Select Id Conductor Notificacion
        public Notificacion buscaridConductorN(double idConductor)//S
        {
            return new MapeoConductor().notificacion.Where(x => x.IdConductor == idConductor).FirstOrDefault();
        }


        //Select Id Conductor Conductor
        public Conductor buscaridConductorCo(double idConductor)//S
        {
            return new MapeoConductor().conduc.Where(x => x.IdConductor == idConductor).FirstOrDefault();
        }

        //Select Id Cliente Notificacion
        public Notificacion buscaridClienteN(double idCliente)
        {
            return new MapeoCliente().notificacion.Where(x => x.IdCliente == idCliente).FirstOrDefault();
        }

        //Select Id Cliente cliente
        public Cliente buscaridClienteC(double idCliente)
        {
            return new MapeoCliente().client.Where(x => x.IdCliente == idCliente).FirstOrDefault();
        }

        //Inserta Registro Administrador
        public void inserAdmin(Admin admin)
        {
            using (var db = new MapeoAdministrador())
            {
                admin.Rol = 3;
                db.administrador.Add(admin);
                db.SaveChanges();
            }
        }
    }
}
