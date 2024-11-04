using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuvar
{
    public class Fuvar
    {
        public int TaxiId { get; private set; }
        public DateTime Indulas { get; private set; }
        public int Ido { get; private set; }
        public double Tavolsag { get; private set; }
        public double Viteldij { get; private set; }
        public double Borravalo { get; private set; }
        public string FizetesModja { get; private set; }

        public Fuvar(int taxiId, DateTime indulas, int ido, double tavolsag, double viteldij, double borravalo, string fizetesModja)
        {
            TaxiId = taxiId;
            Indulas = indulas;
            Ido = ido;
            Tavolsag = tavolsag;
            Viteldij = viteldij;
            Borravalo = borravalo;
            FizetesModja = fizetesModja;
        }

        public static List<Fuvar> LoadFromCsv(string fileName)
        {
            var fuvarok = new List<Fuvar>();
            var lines = File.ReadAllLines(fileName);
            for (int i = 1; i < lines.Length; i++) 
            {
                var fields = lines[i].Split(';');
                var fuvar = new Fuvar(
                    int.Parse(fields[0]),
                    DateTime.ParseExact(fields[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    int.Parse(fields[2]),
                    double.Parse(fields[3], CultureInfo.InvariantCulture),
                    double.Parse(fields[4], CultureInfo.InvariantCulture),
                    double.Parse(fields[5], CultureInfo.InvariantCulture),
                    fields[6]
                );
                fuvarok.Add(fuvar);
            }
            return fuvarok;
        }
    }
}
