using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compi_Proyecto_1
{
    public class Interval
    {
        public char origin;
        public char destiny;
        public Interval(char origin, char destiny)
        {
            this.origin = origin;
            this.destiny = destiny;
        }
    }

    public class Set
    {
        String pattern;
        String lexical_component;
        public List<String> elements1;
        public Interval elements2;

        public Set(String pattern, String lexical_component)
        {
            this.pattern = pattern;
            this.lexical_component = lexical_component;
        }

        public string get_lexical_component()
        {
            return this.lexical_component;
        }

        public void analize_pattern()
        {
            char character;
            elements1 = new List<string>();
            int start = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                character = pattern.ElementAt(i);
                if (character == ',')
                {
                    elements1.Add(pattern.Substring(start , i - start));
                    start = i;
                }
                else if (character == '~')
                {
                    string inter1 = pattern.Substring(start, i - start);
                    string inter2 = pattern.Substring(i + 1, (pattern.Count()-1) - i);
                    if (inter1.Length > 1 || inter2.Length > 1)
                        interval_numbers(inter1, inter2);
                    elements2 = new Interval(pattern.ElementAt(i - 1), pattern.ElementAt(i + 1));
                    break;
                }
                else if (i == pattern.Length - 1)
                {
                    elements1.Add(pattern.Substring(start, i - start));
                }
            }
        }
        public void interval_numbers(string inter1, string inter2)
        {
            int inter1_number = int.Parse(inter1);
            int inter2_number = int.Parse(inter2);
            for(int i = inter1_number; i <= inter2_number; i++)
                elements1.Add(i.ToString());
        }
    }
}
