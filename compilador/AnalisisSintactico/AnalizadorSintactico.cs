using compilador.AnalisisLexico;
using compilador.ManejadorErrores;
using compilador.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compilador.AnalisisSintactico
{
    class AnalizadorSintactico
    {
        private AnalizadorLexico AnaLex = new AnalizadorLexico();
        private ComponenteLexico Componente;
        private StringBuilder Traza;

        public void Analizar(bool Depurar)
        {
            Avanzar();
            Traza = new StringBuilder();
            Expresion(1);

            if (Depurar)
            {
                MessageBox.Show(Traza.ToString());
            }
            if (GestorErrores.ObtenerInstancia().HayErrores())
            {
                MessageBox.Show("La compilación ha finalizado, pero hay errores en el programa de entrada ingresado.");
            }
            else if (Categoria.FIN_ARCHIVO.Equals(Componente.ObtenerCategoria()))
            {
                MessageBox.Show("La compilación ha finalizado exitosamente.");
            }
            else
            {
                MessageBox.Show("La compilación ha finalizado exitosamente, faltaron componentes por evaluar.");
            }



        }

        private void Avanzar()
        {
            Componente = AnaLex.DevolverComponenteLexico();
        }

        private void FormarEntrada(int Nivel, string Regla)
        {

            for (int Contador = 1; Contador <= Nivel * 5; Contador++)
            {
                Traza.Append("-");
            }
            Traza.Append(">");
            Traza.Append("INICIO ").Append(Regla);
            Traza.Append("\n");
            FormarComponente(Nivel);
        }

        private void FormarSalida(int Nivel, string Regla)
        {
            Traza.Append("<");
            for (int Contador = 1; Contador <= Nivel * 5; Contador++)
            {
                Traza.Append("-");
            }

            Traza.Append("FIN ").Append(Regla);
            Traza.Append("\n");

        }

        private void FormarComponente(int Nivel)
        {
            Traza.Append("-");
            for (int Contador = 1; Contador <= (Nivel + 1) * 5; Contador++)
            {
                Traza.Append("-");
            }

            Traza.Append("Componente actual: ").Append(this.Componente.ObtenerLexema()).Append("/").Append(this.Componente.ObtenerCategoria()).Append("\n");
        }


        private void Expresion(int Nivel)
        {
            FormarEntrada(Nivel, "<Expresión>");
            Termino(Nivel + 1);
            ExpresionPrima(Nivel + 1);
            FormarSalida(Nivel, "<Expresion>");
        }

        private void ExpresionPrima(int Nivel)
        {
            if (Categoria.SUMA.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
                Expresion(Nivel + 1);
            }
            else if (Categoria.RESTA.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
                Expresion(Nivel + 1);
            }

        }

        private void Termino(int Nivel)
        {
            FormarEntrada(Nivel, "<Término>");
            Factor(Nivel + 1);
            TerminoPrima(Nivel + 1);
            FormarSalida(Nivel, "<Término>");
        }

        private void TerminoPrima(int Nivel)
        {
            if (Categoria.MULTIPLICACION.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
                Termino(Nivel + 1);
            }
            else if (Categoria.DIVISION.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
                Termino(Nivel + 1);
            }
        }

        private void Factor(int Nivel)
        {
            if (Categoria.NUMERO_ENTERO.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
            }
            else if (Categoria.NUMERO_DECIMAL.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
            }
            else if (Categoria.PARENTESIS_ABRE.Equals(Componente.ObtenerCategoria()))
            {
                Avanzar();
                Expresion(Nivel + 1);
                if (Categoria.PARENTESIS_CIERRA.Equals(Componente.ObtenerCategoria()))
                {
                    Avanzar();
                }
                else
                {
                    String Falla = "Componente no válido: " + Componente.ObtenerLexema();
                    String Causa = "Recibí " + Componente.ObtenerCategoria() + " y esperaba ), paréntesis cierra.";
                    String Solucion = "Asegúrese de que el paréntesis que cierra esté ubicado en esta posición.";

                    Error Error = ManejadorErrores.Error.Crear(Componente.ObtenerNumeroLinea(), Componente.ObtenerPosicionInicial(), Componente.ObtenerPosicionFinal(), Falla, Causa, Solucion, ManejadorErrores.TipoError.SINTACTICO);
                    GestorErrores.ObtenerInstancia().Agregar(Error);
                }
            }
            else
            {
                String Falla = "Componente no válido: " + Componente.ObtenerLexema();
                String Causa = "Recibí " + Componente.ObtenerCategoria() + " ...";
                String Solucion = "Asegúrese de que el componente que está en esta posición sea un número entero, decimal o paréntesis abre..";


                Error Error = ManejadorErrores.Error.Crear(Componente.ObtenerNumeroLinea(), Componente.ObtenerPosicionInicial(), Componente.ObtenerPosicionFinal(), Falla, Causa, Solucion, ManejadorErrores.TipoError.SINTACTICO);
                GestorErrores.ObtenerInstancia().Agregar(Error);

                throw new Exception("Error tipo stopper durante el análisis sintáctico. Por favor verifique e intente de nuevo.");
            }
        }
    }
}
