using compilador.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador.TablaSimbolos
{
    public class TablaDummy
    {
        
        private Dictionary<string, List<ComponenteLexico>> Tabla = new Dictionary<string, List<ComponenteLexico>>();
        private static TablaDummy INSTANCIA = new TablaDummy();

        private TablaDummy()
        {

        }

        public static TablaDummy ObtenerInstancia()
        {
            return INSTANCIA;
        }

        public void Limpiar()
        {
            Tabla.Clear();
        }

        private List<ComponenteLexico> ObtenerDummy(string Dummy)
        {
            if (!Tabla.ContainsKey(Dummy))
            {
                Tabla.Add(Dummy, new List<ComponenteLexico>());
            }
            return Tabla[Dummy];
        }

        public void Agregar(ComponenteLexico Componente)
        {
            if (Componente != null && Tipo.DUMMY.Equals(Componente.ObtenerTipo()))
            {
                ObtenerDummy(Componente.ObtenerLexema()).Add(Componente);
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
