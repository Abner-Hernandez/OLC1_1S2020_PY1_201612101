using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compi_Proyecto_1
{
    class Interval
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
        List<char> elements1;
        Interval elements2;

        public Set(String pattern, String lexical_component)
        {
            this.pattern = pattern;
            this.lexical_component = lexical_component;
        }

        public Boolean check_element(char element)
        {
            if (elements1 != null)
            {
                foreach (char data in elements1)
                {
                    if (element == data)
                        return true;
                }
            }
            if (elements2 != null)
            {
                if (elements2.origin <= element && elements2.destiny >= element)
                    return true;
            }
            return false;
        }

        public void analize_pattern()
        {
            char character;
            elements1 = new List<char>();
            for (int i = 0; i < pattern.Length; i++)
            {
                character = pattern.ElementAt(i);
                if (character == ',')
                {
                    elements1.Add(pattern.ElementAt(i - 1));
                }
                else if (character == '~')
                {
                    elements2 = new Interval(pattern.ElementAt(i - 1), pattern.ElementAt(i + 1));
                    break;
                }
                else if (i == pattern.Length - 1)
                {
                    elements1.Add(pattern.ElementAt(i));
                }
            }
        }
    }
}
