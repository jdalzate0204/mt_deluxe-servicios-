namespace Utilitarios
{
    public class Cascaron
    {
        private string mensaje;
        private int destino;
        private int ubicacion;
        private string kilometros;
        private string pago;
        private string url;
        private Cliente cliente;
        private TokenCliente token;
        private Conductor conductor;
        private TokenConductor tokenco;
        private Admin Administrador;
        private string tokenGenerar;

        public string Mensaje { get => mensaje; set => mensaje = value; }
        public string Url { get => url; set => url = value; }
        public int Destino { get => destino; set => destino = value; }
        public int Ubicacion { get => ubicacion; set => ubicacion = value; }
        public string Kilometros { get => kilometros; set => kilometros = value; }
        public string Pago { get => pago; set => pago = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public TokenCliente Token { get => token; set => token = value; }
        public Conductor Conductor { get => conductor; set => conductor = value; }
        public TokenConductor Tokenco { get => tokenco; set => tokenco = value; }
        public Admin Administrador1 { get => Administrador; set => Administrador = value; }
        public string TokenGenerar { get => tokenGenerar; set => tokenGenerar = value; }
    }
}
