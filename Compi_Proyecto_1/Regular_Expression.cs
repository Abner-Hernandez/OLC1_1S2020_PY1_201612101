using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compi_Proyecto_1
{

    class Transition
    {
        public String origin;
        public String destination;
        public String non_terminal;

        public Transition(String origin, String destination, String non_terminal)
        {
            this.origin = origin;
            this.destination = destination;
            this.non_terminal = non_terminal;
        }
        public Transition()
        {
            this.origin = null;
            this.destination = null;
            this.non_terminal = null;
        }

    }


    class Regular_Expression
    {
        String pattern;
        String lexical_component;
        String txtfile;
        Stack<Node> elements;
        Stack<Transition> recursive;
        Node root;
        List<Transition> transitions;

        public Regular_Expression(String pattern, String lexical_comp)
        {
            this.lexical_component = lexical_comp;
            this.pattern = pattern;
            this.elements = new Stack<Node>();
            this.root = new Node();
            this.transitions = new List<Transition>();
            this.recursive = null;
            this.txtfile = null;
        }

        public string get_lexical_component()
        {
            return lexical_component;
        }

        public void generate_afn()
        {
            int size_pattern = 0, pointer = pattern.Length - 1, initial_pointer = 0;
            char actual;

            while (true)
            {
                actual = pattern.ElementAt(pointer);
                switch (actual)
                {
                    case '}':
                        initial_pointer = pointer;
                        while (pattern.ElementAt(pointer) != '{')
                            pointer--;
                        elements.Push(new Node(pattern.Substring(pointer + 1, initial_pointer - (pointer + 1))));
                        break;
                    case '+':
                        if (elements.Count > 0)
                        {
                            Node insert;
                            Node aux = elements.Pop();
                            if (aux.is_a_terminal())
                                insert = aux.create_more_lock(aux.non_terminal);
                            else
                                insert = aux.create_more_lock(aux);
                            elements.Push(insert);
                        }
                        break;
                    case '?':
                        if (elements.Count > 0)
                        {
                            Node insert;
                            Node aux = elements.Pop();
                            if (aux.is_a_terminal())
                                insert = aux.create_quest_lock(aux.non_terminal);
                            else
                                insert = aux.create_quest_lock(aux);
                            elements.Push(insert);
                        }
                        break;
                    case '*':
                        if (elements.Count > 0)
                        {
                            Node insert;
                            Node aux = elements.Pop();
                            if (aux.is_a_terminal())
                                insert = aux.create_Kleene_lock(aux.non_terminal);
                            else
                                insert = aux.create_Kleene_lock(aux);
                            elements.Push(insert);
                        }
                        break;
                    case '|':
                        if (elements.Count > 1)
                        {
                            Node insert;
                            Node aux = elements.Pop();
                            Node aux1 = elements.Pop();
                            if (aux.is_a_terminal() && aux1.is_a_terminal())
                                insert = aux.create_or(aux.non_terminal, aux1.non_terminal);
                            else if(!aux.is_a_terminal() && aux1.is_a_terminal())
                                insert = aux.create_or(aux, aux1.non_terminal);
                            else
                                insert = aux.create_or(aux.non_terminal, aux1);
                            elements.Push(insert);
                        }
                        break;
                    case '.':
                        if (elements.Count > 1)
                        {
                            Node insert;
                            Node aux = elements.Pop();
                            Node aux1 = elements.Pop();
                            if (aux.is_a_terminal() && aux1.is_a_terminal())
                                insert = aux.create_union(aux.non_terminal, aux1.non_terminal);
                            else if (!aux.is_a_terminal() && aux1.is_a_terminal())
                                insert = aux.create_union(aux, aux1.non_terminal);
                            else
                                insert = aux.create_union(aux.non_terminal, aux1);
                            elements.Push(insert);
                        }
                        break;
                    case '"':
                        initial_pointer = pointer--;
                        while (pattern.ElementAt(pointer) != '"')
                            pointer--;
                        elements.Push(new Node(pattern.Substring(pointer + 1, initial_pointer - (pointer + 1))));
                        break;
                    case ' ':
                        break;
                }
                pointer--;
                if (pointer < size_pattern)
                {
                    this.root = elements.Pop();                 
                    break;
                }
            }
        }

        public void enumerate_afn()
        {
            int num = 0;
            this.root.enumerate_afn(this.root, num);
        }

        public void create_afn_graph()
        {
            string ruta = Application.StartupPath + "\\Images\\";
            recursive = new Stack<Transition>();
            txtfile = "digraph Mass{\nrankdir=\"LR\";\n";
            txtfile += "node[height = 1, width = 1]; \n";
            create_afn_graph(this.root, "none");
            txtfile += "\n}";
            save_file(ruta, lexical_component + "afn");
            recursive = null;
        }

        public void save_file(string path, string type)
        {
            System.IO.File.WriteAllText(path + type + ".dot", txtfile);
            string command = "/c dot -Tjpg \"" + path + type + ".dot\" -o \"" + path + type + ".jpg\"";

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;
            process.Start();

            System.Threading.Thread.Sleep(1000);
        }

        public void create_afn_graph(Node pivot, string previos_node)
        {
            txtfile += "node"+ pivot.get_id_node().ToString() + "[label= \"" + pivot.get_id_node().ToString() + "\"];\n";
            if(!previos_node.Equals("none"))
                txtfile += previos_node + " -> " + "node" + pivot.get_id_node().ToString() + "[ label=\"" + pivot.get_non_terminal() + "\" ];\n";
            recursive_graph(pivot);

            string previos_id = "node" + pivot.get_id_node().ToString();
            if (pivot.get_is_or())
            {
                Node aux = pivot.get_nexts()[1];
                string aux_or = "node" + pivot.get_id_node().ToString();
                
                for (int i = 1; i < pivot.get_nexts().Count; i++)
                {
                    aux = pivot.get_nexts()[i];
                    txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    txtfile += aux_or + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\"" + aux.get_non_terminal() + "\" ];\n";
                    previos_id = "node" + aux.get_id_node().ToString();
                    recursive_graph(aux);

                    while (aux.get_nexts().Count > 0)
                    {
                        aux = aux.get_nexts()[0];
                        txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                        txtfile += previos_id + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\""+ aux.get_non_terminal() +"\" ];\n";
                        previos_id = "node" + aux.get_id_node().ToString();
                        recursive_graph(aux);

                        if (aux.get_is_or())
                            create_afn_graph(aux, previos_id);
                    }
                    //txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    txtfile += "node" + aux.get_id_node().ToString() + " -> " + "node" + pivot.get_nexts()[0].get_id_node().ToString() + "[ label=\"" + pivot.get_nexts()[0].get_non_terminal() + "\" ];\n";
                    previos_id = "node" + aux.get_id_node().ToString();
                }
                aux = pivot.get_nexts()[0];
                create_afn_graph(aux, "none");

            }
            else
            {
                Node aux = pivot.get_nexts()[0];
                while (aux.get_nexts().Count > 0)
                {

                    txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    txtfile += previos_id + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\"" + aux.get_non_terminal() + "\" ];\n";
                    previos_id = "node" + aux.get_id_node().ToString();
                    recursive_graph(aux);

                    aux = aux.get_nexts()[0];
                    if (aux.get_is_or())
                    {
                        create_afn_graph(aux, previos_id);
                        break;
                    }
                }if(aux.get_nexts().Count == 0 && aux.get_non_terminal() != null)
                {
                    txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    txtfile += previos_id + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\"" + aux.get_non_terminal() + "\" ];\n";
                    recursive_graph(aux);
                }
            }

        }
        public void recursive_graph(Node node)
        {
            if (node.get_start_empty())
                recursive.Push(new Transition("node" + node.get_id_node().ToString(), null, null));
            else if (node.get_end_recursive())
                recursive.Push(new Transition(null, "node" + node.get_id_node().ToString(), null));
            else if (node.get_end_empty())
                txtfile += recursive.Pop().origin + " -> node" + node.get_id_node().ToString() + "[ label=\"epsilon\" ];\n";
            else if (node.get_start_recursive())
                txtfile += "node" + node.get_id_node().ToString() + " -> " + recursive.Pop().destination + "[ label=\"epsilon\" ];\n";
        }

    }
}
