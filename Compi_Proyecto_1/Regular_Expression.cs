using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compi_Proyecto_1
{
    public class Token
    {
        public string name;
        public string value;
        public int row;
        public int column;

        public Token(string name, string value, int row, int column)
        {
            this.name = name;
            this.value = value;
            this.row = row;
            this.column = column;
        }
        public Token()
        {
            this.name = null;
            this.value = null;
            this.row = -1;
            this.column = -1;
        }
    }

    public class Error
    {
        public string value;
        public int column;
        public int row;

        public Error(string value, int column, int row)
        {
            this.value = value;
            this.row = row;
            this.column = column;
        }
        public Error()
        {
            this.value = null;
            this.row = -1;
            this.column = -1;
        }
    }

    class Transition
    {
        public String origin;
        public String destination;
        public String non_terminal;
        public List<int> orig;
        public List<int> dest;

        public Transition(String origin, String destination, String non_terminal)
        {
            this.origin = origin;
            this.destination = destination;
            this.non_terminal = non_terminal;
            this.orig = null;
            this.dest = null;
        }
        public Transition()
        {
            this.origin = null;
            this.destination = null;
            this.non_terminal = null;
            this.orig = null;
            this.dest = null;
        }
        public Transition(List<int> orig, List<int> dest, string non_terminal)
        {
            this.origin = null;
            this.destination = null;
            this.non_terminal = non_terminal; ;
            this.orig = orig;
            this.dest = dest;
        }
    }

    class Node_Transition
    {
        public int origin;
        public int destination;
        public string non_terminal;
        public Node_Transition(int origin, int destination, string non_terminal)
        {
            this.origin = origin;
            this.destination = destination;
            this.non_terminal = non_terminal;
        }
    }

    class State_Transition
    {
        public char state;
        public List<int> elements;
        public State_Transition()
        {
            this.state = '*';
            this.elements = new List<int>();
        }
        public State_Transition(char actual, List<int> elementos)
        {
            this.state = actual;
            this.elements = elementos;
        }
    }


    class Regular_Expression
    {
        String pattern;
        String lexical_component;
        String txtfile;
        Stack<Node> elements;
        Stack<Node_Transition> recursive;
        Node root;
        List<Transition> transitions;
        List<Node_Transition> nodes_transitions;
        List<string> non_terminals;
        Queue<State_Transition> transition;
        List<State_Transition> all_Transitions;
        char actual;
        List<List<int>> previous_t;
        int number_accept;
        List<char> states_accept;

        public Regular_Expression(String pattern, String lexical_comp)
        {
            this.lexical_component = lexical_comp;
            this.pattern = pattern;
            this.elements = new Stack<Node>();
            this.root = new Node();
            this.transitions = new List<Transition>();
            this.nodes_transitions = new List<Node_Transition>();
            this.recursive = null;
            this.txtfile = null;
            this.non_terminals = null;
            this.actual = 'A';
            this.all_Transitions = null;
            this.previous_t = null;
            this.number_accept = -1;
            this.states_accept = null;
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
                if(pointer > 0)
                {
                    string sub = pattern.Substring((pointer - 1), 2);
                    if (sub.Equals("\\\""))
                    {
                        pointer -= 2;
                        elements.Push(new Node(sub));
                    }
                    else if (sub.Equals("\\\'"))
                    {
                        pointer -= 2;
                        elements.Push(new Node(sub));
                    }
                    else if (sub.Equals("\\t"))
                    {
                        pointer -= 2;
                        elements.Push(new Node("tabulacion"));
                    }
                    else if (sub.Equals("\\n"))
                    {
                        pointer -= 2;
                        elements.Push(new Node("salto linea"));
                    }
                    if((pointer -7) >= 0)
                    {
                        sub = pattern.Substring((pointer - 7), 8);
                        if (sub.Equals("[:todo:]"))
                        {
                            pointer -= 8;
                            elements.Push(new Node("todo"));
                        }
                    }
                }

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
            this.number_accept = this.root.get_accept_number(this.root);
        }

        public void create_afn_graph()
        {
            recursive = new Stack<Node_Transition>();
            non_terminals = new List<string>();
            txtfile = "digraph Mass{\nrankdir=\"LR\";\n";
            txtfile += "node[height = 1, width = 1]; \n";
            create_afn_graph(this.root, -1);
            txtfile += "\n}";
            save_file(Application.StartupPath + "\\Images\\", lexical_component + "_afn");
            recursive = null;
            get_nonterminals();
            graph_table_transitions();
            get_accept_states();
            graph_automaton();
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

        public void create_afn_graph(Node pivot, int previos_node)
        {
            txtfile += "node"+ pivot.get_id_node().ToString() + "[label= \"" + pivot.get_id_node().ToString() + "\"];\n";
            if (previos_node != -1)
            {
                txtfile += "node" + previos_node + " -> " + "node" + pivot.get_id_node().ToString() + "[ label=\"" + pivot.get_non_terminal() + "\" ];\n";
                nodes_transitions.Add(new Node_Transition(previos_node, pivot.get_id_node(), pivot.get_non_terminal()));
            }
            non_terminals.Add(pivot.get_non_terminal());
            recursive_graph(pivot);

            int previos_id = pivot.get_id_node();
            if (pivot.get_is_or())
            {
                Node aux = pivot.get_nexts()[1];
                string aux_or = "node" + pivot.get_id_node().ToString();
                
                for (int i = 1; i < pivot.get_nexts().Count; i++)
                {
                    aux = pivot.get_nexts()[i];
                    txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    txtfile += aux_or + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\"" + aux.get_non_terminal() + "\" ];\n";
                    nodes_transitions.Add(new Node_Transition(pivot.get_id_node(), aux.get_id_node(), aux.get_non_terminal()));
                    non_terminals.Add(aux.get_non_terminal());
                    previos_id = aux.get_id_node();
                    recursive_graph(aux);

                    while (aux.get_nexts().Count > 0)
                    {
                        aux = aux.get_nexts()[0];
                        txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                        txtfile += "node" + previos_id + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\""+ aux.get_non_terminal() +"\" ];\n";
                        nodes_transitions.Add(new Node_Transition(previos_id, aux.get_id_node(), aux.get_non_terminal()));
                        previos_id = aux.get_id_node();
                        non_terminals.Add(aux.get_non_terminal());
                        recursive_graph(aux);

                        if (aux.get_is_or())
                            create_afn_graph(aux, previos_id);
                    }
                    txtfile += "node" + aux.get_id_node().ToString() + " -> " + "node" + pivot.get_nexts()[0].get_id_node().ToString() + "[ label=\"" + pivot.get_nexts()[0].get_non_terminal() + "\" ];\n";
                    nodes_transitions.Add(new Node_Transition(aux.get_id_node(), pivot.get_nexts()[0].get_id_node(), pivot.get_nexts()[0].get_non_terminal()));
                    previos_id = aux.get_id_node();
                }
                aux = pivot.get_nexts()[0];
                non_terminals.Add(aux.get_non_terminal());
                create_afn_graph(aux, -1);

            }
            else
            {
                Node aux = pivot.get_nexts()[0];
                while (aux.get_nexts().Count > 0)
                {

                    txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    txtfile += "node" + previos_id + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\"" + aux.get_non_terminal() + "\" ];\n";
                    nodes_transitions.Add(new Node_Transition(previos_id, aux.get_id_node(), aux.get_non_terminal()));
                    non_terminals.Add(aux.get_non_terminal());
                    previos_id = aux.get_id_node();
                    recursive_graph(aux);

                    aux = aux.get_nexts()[0];
                    if (aux.get_is_or())
                    {
                        create_afn_graph(aux, previos_id);
                        break;
                    }
                }if(aux.get_nexts().Count == 0 && aux.get_non_terminal() != null)
                {
                    if(this.number_accept != aux.get_id_node())
                        txtfile += "node" + aux.get_id_node().ToString() + "[label= \"" + aux.get_id_node().ToString() + "\"];\n";
                    else
                        txtfile += "node" + aux.get_id_node().ToString() + "[shape = doublecircle label= \"" + aux.get_id_node().ToString() + "\"];\n";

                    txtfile += "node" + previos_id + " -> " + "node" + aux.get_id_node().ToString() + "[ label=\"" + aux.get_non_terminal() + "\" ];\n";
                    nodes_transitions.Add(new Node_Transition(previos_id, aux.get_id_node(), aux.get_non_terminal()));
                    non_terminals.Add(aux.get_non_terminal());
                    recursive_graph(aux);
                }
            }

        }

        public void recursive_graph(Node node)
        {
            if (node.get_start_empty())
                recursive.Push(new Node_Transition(node.get_id_node(), -1, "epsilon"));
            else if (node.get_end_recursive())
                recursive.Push(new Node_Transition(-1, node.get_id_node(), "epsilon"));
            else if (node.get_end_empty())
            {
                int origin = recursive.Pop().origin;
                nodes_transitions.Add(new Node_Transition(origin, node.get_id_node(), "epsilon"));
                txtfile += "node" + origin.ToString() + " -> node" + node.get_id_node().ToString() + "[ label=\"epsilon\" ];\n";
            }
            else if (node.get_start_recursive())
            {
                int destination = recursive.Pop().destination;
                nodes_transitions.Add(new Node_Transition(node.get_id_node(), destination, "epsilon"));
                txtfile += "node" + node.get_id_node().ToString() + " -> node" + destination.ToString() + "[ label=\"epsilon\" ];\n";
            }
        }

        private List<int> delete_dupplicate(List<int> list)
        {
            return ((from s in list select s).Distinct()).ToList();
        }

        private List<string> delete_dupplicate(List<string> list)
        {
            return ((from s in list select s).Distinct()).ToList();
        }

        public void get_nonterminals()
        {
            List<string> sin_repetir = delete_dupplicate(this.non_terminals);
            sin_repetir.Remove("none");
            sin_repetir.Remove("epsilon");
            this.non_terminals = sin_repetir;
            transition = new Queue<State_Transition>();
            all_Transitions = new List<State_Transition>();
            previous_t = new List<List<int>>();
            List<int> initial = new List<int>();
            initial.Add(0);
            previous_t.Add(initial);
            cerradura_epsilon(initial);
            foreach(Transition trans in transitions)
            {
                trans.origin = verificate_State(trans.orig).ToString();
                trans.destination = verificate_State(trans.dest).ToString();
            }
        }

        public void cerradura_epsilon(List<int> actual)
        {
            List<int> elements = get_all_epsilon(actual);

            if (verificate_State(elements) == '*')
                all_Transitions.Add(new State_Transition(this.actual++, elements));
            go_transition(elements);
            
        }

        public List<int> get_all_epsilon(List<int> actual)
        {
            List<int> elements = new List<int>();
            foreach (int act in actual)
            {
                elements.Add(act);
                Queue<int> look_epsilon = new Queue<int>();
                foreach (Node_Transition t in nodes_transitions)
                {
                    if (act == t.origin && t.non_terminal.Equals("epsilon"))
                    {
                        elements.Add(t.destination);
                        look_epsilon.Enqueue(t.destination);
                    }
                }
                verificate_epsilon(look_epsilon, elements);
            }
            elements = delete_dupplicate(elements);
            elements.Sort();
            return elements;
        }

        public void verificate_epsilon(Queue<int> initial, List<int> elements)
        {
            while (initial.Count > 0)
            {
                int act = initial.Dequeue();
                foreach (Node_Transition t in nodes_transitions)
                {
                    if (act == t.origin && t.non_terminal.Equals("epsilon"))
                    {
                        elements.Add(t.destination);
                        initial.Enqueue(t.destination);
                    }
                }
            }
        }

        private void go_transition(List<int> actual)
        {
            foreach (string no_terminal in non_terminals)
            {
                List<int> elements = new List<int>();
                foreach (int act in actual)
                {
                    foreach (Node_Transition t in nodes_transitions)
                    {
                        if (act == t.origin && no_terminal.Equals(t.non_terminal))
                        {
                            elements.Add(t.destination);
                        }
                    }
                }
                if(elements.Count > 0)
                {
                    elements = delete_dupplicate(elements);
                    elements.Sort();
                    
                    if (verificate_State(get_all_epsilon(elements)) == '*' && !verificate_previous_states(elements))
                    {
                        previous_t.Add(elements);
                        transition.Enqueue(new State_Transition('*', elements));
                    }
                    transitions.Add(new Transition(actual, get_all_epsilon(elements) , no_terminal));
                }
            }
            while(transition.Count != 0)
            {
                cerradura_epsilon(transition.Dequeue().elements);
            }
        }

        private char verificate_State(List<int> comp)
        {
            foreach(State_Transition state in all_Transitions)
            {
                if (state.elements.Count != comp.Count)
                    continue;
                List<int> distinct = comp.Except(state.elements).ToList();
                if (distinct.Count == 0)
                    return state.state;
            }
            return '*';
        }

        private Boolean verificate_previous_states(List<int> compare)
        {
            foreach(List<int> actual in previous_t)
            {
                if (actual.Count != compare.Count)
                    continue;
                List<int> distinct = actual.Except(compare).ToList();
                if (distinct.Count == 0)
                    return true;
            }
            return false;
        }

        private void get_accept_states()
        {
            states_accept = new List<char>();
            foreach (State_Transition state in all_Transitions)
            {

                foreach(int actual in state.elements)
                {
                    if (actual == this.number_accept)
                    {
                        states_accept.Add(state.state);
                        break;
                    }

                }
            }
        }

        public void graph_table_transitions()
        {
            txtfile = "digraph Mass{\n";
            txtfile += "aHtmlTable [\nshape=plaintext\ncolor=black\n";
            txtfile += "label=<\n";

            txtfile += "<table border='1' cellborder='1'>\n";
            txtfile += "<tr><td>Estado</td><td colspan='" + (non_terminals.Count).ToString() + "'>Terminales</td></tr>\n";

            txtfile += "<tr><td></td>";
            for (int i = 0; i < non_terminals.Count ; i++)
                txtfile += "<td>" + non_terminals.ElementAt(i) + "</td>";
            txtfile += "</tr>\n";

            for (char i = 'A'; i < actual; i++)
            {

                txtfile += "<tr><td>" + i.ToString() + "</td>";
                for (int j = 0; j < non_terminals.Count; j++)
                {
                    Boolean exist = false;
                    foreach (Transition transition in transitions)
                    {
                        if (transition.origin.Equals(i.ToString()) && non_terminals.ElementAt(j).Equals(transition.non_terminal))
                        {
                            exist = true;
                            txtfile += "<td>" + transition.destination + "</td>";
                        }
                    }
                    if (!exist)
                        txtfile += "<td> - </td>";
                }
                txtfile += "</tr>\n";
            }
            txtfile += "</table>\n";
            txtfile += "\n>\n];\n}";
            save_file(Application.StartupPath + "\\Images\\", lexical_component + "_transitions");
        }

        public void graph_automaton()
        {
            if (transitions.Count > 0)
            {
                txtfile = "digraph Mass{\n";
                txtfile += "node[height = 1, width = 1]; \n";
                for(char i = 'A'; i < actual; i++)
                {
                    if(!aceptation(i))
                        txtfile += i.ToString() + "[label= \"" + i.ToString() + "\"];\n";
                    else
                        txtfile += i.ToString() + "[shape = doublecircle label= \"" + i.ToString() + "\"];\n";


                }
                foreach (Transition transition in transitions)
                    txtfile += transition.origin + " -> " + transition.destination + "[label=\"  " + transition.non_terminal + "\" ];\n";
                txtfile += "\n}";
                save_file(Application.StartupPath + "\\Images\\", lexical_component + "_afd");
            }
        }

        public string analize_lexeme(String lexeme, List<Set> sets, List<Error> errors, string name)
        {
            String actual = transitions.ElementAt(0).origin;
            int row = 0, column = 0;
            int ingreso = 0;
            string error = "";
            for (int i = 0; i < lexeme.Count(); i++)
            {
                ingreso = i;
                for (int j = 0; j < transitions.Count(); j++)
                {
                    if (transitions.ElementAt(j).origin.Equals(actual))
                    {
                        String data2 = transitions.ElementAt(j).non_terminal;

                        if (data2.Equals("todo"))
                        {
                            if (lexeme.ElementAt(i) != '\n' && lexeme.ElementAt(i) != '\r')
                            {
                                i++;
                                actual = transitions.ElementAt(j).destination;
                                break;
                            }
                        }
                        else if (data2.Equals("tabulacion") && lexeme.ElementAt(i) == '\t')
                        {
                            i++;
                            actual = transitions.ElementAt(j).destination;
                            break;
                        }
                        else if (data2.Equals("salto linea") && lexeme.Substring(i,2) == "\r\n")
                        {
                            row++;
                            column = i;
                            i += 2;
                            actual = transitions.ElementAt(j).destination;
                            break;
                        }
                        else if (data2.Equals("\\'") && lexeme.ElementAt(i) == '\'')
                        {
                            i++;
                            actual = transitions.ElementAt(j).destination;
                            break;
                        }

                        Boolean set = verificate_sets(data2, sets);
                        if (set)
                        {
                            Boolean exit = false;
                            foreach (Set set_actual in sets)
                            {

                                if (set_actual.get_lexical_component().Equals(data2))
                                {
                                    if (set_actual.elements1.Count() > 0)
                                    {
                                        foreach (string data in set_actual.elements1)
                                        {
                                            string datass = "";
                                            if (lexeme.Count() >= (i+data.Count()))
                                                datass = lexeme.Substring(i, data.Count());

                                            error = datass;

                                            if (datass == data)
                                            {
                                                i = i + data.Count();
                                                actual = transitions.ElementAt(j).destination;
                                                exit = true;
                                                break;
                                            }
                                        }
                                    }else if (set_actual.elements2 != null)
                                    {
                                        if (set_actual.elements2.origin <= lexeme.ElementAt(i) && set_actual.elements2.destiny >= lexeme.ElementAt(i))
                                        {
                                            i++;
                                            actual = transitions.ElementAt(j).destination;
                                            exit = true;
                                            break;
                                        }
                                        else
                                            error = lexeme.ElementAt(i).ToString();
                                    }

                                }
                            }
                            if (exit)
                                break;
                        }
                        else
                        {
                            String data = "";
                            if (lexeme.Count() >= (i + transitions.ElementAt(j).non_terminal.Count()))
                                data = lexeme.Substring(i, transitions.ElementAt(j).non_terminal.Count());

                            error = data;

                            if (data2.Equals(data))
                            {
                                i = i + transitions.ElementAt(j).non_terminal.Count();
                                actual = transitions.ElementAt(j).destination;
                                break;
                            }
                        }
                    }

                }
                if (ingreso == i)
                {
                    errors.Add(new Error("Error Lexico: '" + error + "', Token: " + name, i-column,row));
                    return "Error en la validacion del lexema: \"" + lexeme + "\" con la expresion regular: \"" + lexical_component + "\"\r\n";
                }
                else
                    i--;
            }
            if (aceptation(actual.ElementAt(0)))
                return "La validacion del lexema: \"" + lexeme + "\" fue exitosa con la expresion regular: \"" + lexical_component + "\"\r\n";
            else
                return "";
        }

        private Boolean verificate_sets(String lexical_c, List<Set> sets)
        {
            return verificate_if_set(lexical_c, sets);
        }

        private Boolean verificate_char(char actual, String nonterminal, List<Set> sets)
        {
            return verificate_char_in_set(nonterminal, actual, sets);
        }

        private Boolean aceptation(char state)
        {
            foreach(char actual in states_accept)
            {
                if (actual == state)
                    return true;
            }
            return false;
        }

        private Boolean verificate_char_in_set(String terminal, char character, List<Set> sets)
        {
            return false;
        }

        private Boolean verificate_if_set(String lexical_component, List<Set> sets)
        {
            foreach (Set set in sets)
            {
                if (set.get_lexical_component().Equals(lexical_component))
                    return true;
            }
            return false;
        }

    }
}
