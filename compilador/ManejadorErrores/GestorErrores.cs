﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador.ManejadorErrores
{
    class GestorErrores
    {
        private Dictionary<TipoError, List<Error>> Tabla = new Dictionary<TipoError, List<Error>>();
        private static GestorErrores INSTANCIA = new GestorErrores();

        private GestorErrores()
        {
            Iniciar();
        }

        private void Iniciar()
        {
            Tabla.Clear();
            Tabla.Add(TipoError.LEXICO, new List<Error>());
            Tabla.Add(TipoError.SINTACTICO, new List<Error>());
            Tabla.Add(TipoError.SEMANTICO, new List<Error>());
        }

        public static GestorErrores ObtenerInstancia()
        {
            return INSTANCIA;
        }

        public void Limpiar()
        {
            Iniciar();
        }

        private List<Error> ObtenerSimbolo(string Simbolo)
        {
            if (!Tabla.ContainsKey(Simbolo))
            {
                Tabla.Add(Simbolo, new List<Error>());
            }
            return Tabla[Simbolo];
        }

        public void Agregar(Error Error )
        {
            if (Error != null)
            {
                Tabla[Error.ObtenerTipo()].Add(Error);
            }
        }

        public bool HayErrores(TipoError Tipo)
        {
            return Tabla[Tipo].Count > 0;
        }

        public bool HayErrores()
        {
            return HayErrores(TipoError.LEXICO) || HayErrores(TipoError.SEMANTICO) || HayErrores(TipoError.SINTACTICO);
        }

        public List<Error> ObtenerErrores(TipoError Tipo)
        {
            return Tabla[Tipo];
        }
    }
}