using System;

namespace Labb3._1
{
    public class Pass
    {
        public string Kategori { get; set; } = string.Empty;
        public TimeSpan Tid { get; set; }
        public int BokadePlatser { get; set; }
        public int MaxPlatser { get; set; }


        // Kollar om passet är fullbokat
        public bool ÄrFullbokat => BokadePlatser >= MaxPlatser;

        public bool BokaPlats()
        {
            if (!ÄrFullbokat)
            {
                BokadePlatser++;
                return true;
            }
            return false;
        }

        public bool AvbokaPlats()
        {
            if (BokadePlatser > 0)
            {
                BokadePlatser--;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Kategori} - {Tid:hh\\:mm} ({BokadePlatser}/{MaxPlatser} platser)";
        }
    }
}
