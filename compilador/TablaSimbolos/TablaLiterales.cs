using compilador.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador.TablaSimbolos
{
    public class TablaLiterales
    {
        private Dictionary<string, List<ComponenteLexico>> Tabla = new Dictionary<string, List<ComponenteLexico>>();
        private static TablaLiterales INSTANCIA = new TablaLiterales();

        private TablaLiterales()
        {

        }

        public static TablaLiterales ObtenerInstancia()
        {
            return INSTANCIA;
        }

        public void Limpiar()
        {
            Tabla.Clear();
        }

        private List<ComponenteLexico> ObtenerLiteral(string Literal)
        {
            if (!Tabla.ContainsKey(Literal))
            {
                Tabla.Add(Literal, new List<ComponenteLexico>());
            }
            return Tabla[Literal];
        }

        public void Agregar(ComponenteLexico Componente)
        {
            if (Componente != null && Tipo.LITERAL.Equals(Componente.ObtenerTipo()))
            {
                ObtenerLiteral(Componente.ObtenerLexema()).Add(Componente);

            }
        }

        public List<ComponenteLexico> ObtenerComponentes()
        {
            List<ComponenteLexico> Componentes = new List<ComponenteLexico>();

            foreach (List<ComponenteLexico> Lista in Tabla.Values)
            {
                Componentes.AddRange(Lista);
            }
            return Componentes;
        }
    }
}
