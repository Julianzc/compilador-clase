﻿using compilador.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador.TablaSimbolos
{
    public class TablaDummy
    {
        private Dictionary<string, ComponenteLexico> TablaReservadas = new Dictionary<string, ComponenteLexico>();
        private Dictionary<string, List<ComponenteLexico>> Tabla = new Dictionary<string, List<ComponenteLexico>>();
        private static TablaDummy INSTANCIA = new TablaDummy();

        private TablaDummy()
        {

        }

        public static TablaDummy ObtenerInstancia()
        {
            return INSTANCIA;
        }

        private void Inicializar()
        {
            //Aquí defino todas las palabras reservadas de mi lenguaje
            TablaReservadas.Add("palabra", ComponenteLexico.Crear("palabra", Categoria.PALABRA, Tipo.PALABRA_RESERVADA));
            TablaReservadas.Add("OtraPalabra", ComponenteLexico.Crear("OtraPalabra", Categoria.PALABRA, Tipo.PALABRA_RESERVADA));

        }

        private void ValidarSiComponenteEsPalabraReservada(ComponenteLexico Componente)
        {
            if (Componente != null && TablaReservadas.ContainsKey(Componente.ObtenerLexema()))
            {
                ComponenteLexico PalabraReservada = TablaReservadas[Componente.ObtenerLexema()];
                Componente = ComponenteLexico.Crear(PalabraReservada.ObtenerLexema(), PalabraReservada.ObtenerCategoria(), Componente.ObtenerNumeroLinea(), Componente.ObtenerPosicionInicial(), Componente.ObtenerPosicionFinal(), Tipo.PALABRA_RESERVADA);
            }
        }


        public void Limpiar()
        {
            Tabla.Clear();
        }

        private List<ComponenteLexico> ObtenerSimbolo(string Simbolo)
        {
            if (!Tabla.ContainsKey(Simbolo))
            {
                Tabla.Add(Simbolo, new List<ComponenteLexico>());
            }
            return Tabla[Simbolo];
        }

        public void Agregar(ComponenteLexico Componente)
        {
            if (Componente != null && Tipo.DUMMY.Equals(Componente.ObtenerTipo()))
            {
                ObtenerSimbolo(Componente.ObtenerLexema()).Add(Componente);

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