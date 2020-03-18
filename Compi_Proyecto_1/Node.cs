using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compi_Proyecto_1
{
    class Node
    {
        int id_node;
        public string non_terminal;
        List<Node> nexts;
        Boolean start_recursive;
        Boolean end_recursive;
        Boolean start_empty;
        Boolean end_empty;
        Boolean is_Or;

        public Node(string non_terminal)
        {
            this.id_node = -1;
            this.non_terminal = non_terminal;
            this.nexts = null;
            this.is_Or = false;
            this.start_empty = false;
            this.start_recursive = false;
            this.end_empty = false;
            this.end_recursive = false;
        }
        public Node()
        {
            this.id_node = -1;
            this.non_terminal = null;
            this.nexts = new List<Node>();
            this.is_Or = false;
            this.start_empty = false;
            this.start_recursive = false;
            this.end_empty = false;
            this.end_recursive = false;
        }
        public Node(string non_terminal_transition, Boolean is_Or)
        {
            this.id_node = -1;
            this.non_terminal = non_terminal_transition;
            this.nexts = new List<Node>();
            this.is_Or = is_Or;
            this.start_empty = false;
            this.start_recursive = false;
            this.end_empty = false;
            this.end_recursive = false;
            if (is_Or)
                nexts.Add(new Node("epsilon", false));
        }
        public Node(string non_terminal_transition, Boolean is_Or, Boolean start_empty, Boolean start_recursive, Boolean end_empty, Boolean end_recursive)
        {
            this.id_node = -1;
            this.non_terminal = non_terminal_transition;
            this.nexts = new List<Node>();
            this.is_Or = is_Or;
            this.start_empty = start_empty;
            this.start_recursive = start_recursive;
            this.end_empty = end_empty;
            this.end_recursive = end_recursive;
            if (is_Or)
                nexts.Add(new Node("epsilon", false));
        }

        //get a terminal boolean
        public Boolean is_a_terminal()
        {
            if (nexts == null)
                return true;
            return false;
        }

        //parameters getters
        public int get_id_node()
        {
            return id_node;
        }
        public List<Node> get_nexts()
        {
            return nexts;
        }
        public Boolean get_start_recursive()
        {
            return start_recursive;
        }
        public Boolean get_start_empty()
        {
            return start_empty;
        }
        public Boolean get_end_recursive()
        {
            return end_recursive;
        }
        public Boolean get_end_empty()
        {
            return end_empty;
        }
        public Boolean get_is_or()
        {
            return is_Or;
        }
        public string get_non_terminal()
        {
            return non_terminal;
        }

        //create possibilities to union
        public Node create_union(string first, string second)
        {
            Node init_node = new Node("none", false);
            init_node.nexts.Add(new Node(first, false));
            init_node.nexts[0].nexts.Add(new Node(second, false));
            return init_node;
        }
        public Node create_union(Node first, string second)
        {
            Node aux = first.nexts[0];
            while (aux.nexts.Count > 0)
                aux = aux.nexts[0];
            aux.nexts.Add(new Node(second, false));
            return first;
        }
        public Node create_union(string first, Node second)
        {
            Node init_node = new Node("none", false);
            second.non_terminal = first;
            init_node.nexts.Add(second);
            return init_node;
        }

        //create possibilities to Or
        public Node create_or(string first, string second)
        {
            Node init_node = new Node("none", true);
            Node aux1 = new Node("epsilon", false);
            aux1.nexts.Add(new Node(first, false));
            init_node.nexts.Add(aux1);

            Node aux2 = new Node("epsilon", false);
            aux2.nexts.Add(new Node(second, false));
            init_node.nexts.Add(aux2);
            return init_node;
        }
        public Node create_or(Node first, string second)
        {
            Node aux2 = new Node("epsilon", false);
            aux2.nexts.Add(new Node(second, false));
            first.nexts.Add(aux2);
            return first;
        }
        public Node create_or(string first, Node second)
        {
            Node init_node = new Node("none", true);

            Node aux2 = new Node("epsilon",false);
            aux2.nexts.Add(new Node(first, false));
            init_node.nexts.Add(aux2);

            if (second.is_Or && second.nexts[0].nexts.Count == 0)
            {
                for (int i = 1; i < second.nexts.Count; i++)
                    init_node.nexts.Add(second.nexts[i]);
            }
            else
                init_node.nexts.Add(second);

            return init_node;
        }

        //create possibilities to kleene lock
        public Node create_Kleene_lock(string first)
        {
            Node init_node = new Node("none", false, true, false, false, false);
            init_node.nexts.Add(new Node("epsilon", false, false, false, false, true));
            init_node.nexts[0].nexts.Add(new Node(first, false, false, true, false, false));
            init_node.nexts[0].nexts[0].nexts.Add(new Node("epsilon", false, false, false, true, false));
            return init_node;
        }
        public Node create_Kleene_lock(Node first)
        {
            Node init_node = new Node("none", false, true, false, false,  false);
            first.non_terminal = "epsilon";
            first.end_recursive = true;
            init_node.nexts.Add(first);

            Node aux = first;
            while (aux.nexts.Count > 0)
                aux = aux.nexts[0];
            aux.start_recursive = true;

            aux.nexts.Add(new Node("epsilon", false, false, false, true, false));
            return init_node;
        }

        //create possibilities to + lock
        public Node create_more_lock(string first)
        {
            Node init_node = new Node("none", false);
            init_node.nexts.Add(new Node("epsilon", false, false, false, false, true));
            init_node.nexts[0].nexts.Add(new Node(first, false, false, true, false, false));
            init_node.nexts[0].nexts[0].nexts.Add(new Node("epsilon", false));
            return init_node;
        }
        public Node create_more_lock(Node first)
        {
            Node init_node = new Node("none", false);
            first.non_terminal = "epsilon";
            first.end_recursive = true;
            init_node.nexts.Add(first);

            Node aux = first;
            while (aux.nexts.Count > 0)
                aux = aux.nexts[0];
            aux.start_recursive = true;

            aux.nexts.Add(new Node("epsilon", false));
            return init_node;
        }

        //create possibilities to ? lock
        public Node create_quest_lock(string first)
        {
            Node init_node = new Node("none", false, true, false, false, false);
            init_node.nexts.Add(new Node("epsilon", false));
            init_node.nexts[0].nexts.Add(new Node(first, false));
            init_node.nexts[0].nexts[0].nexts.Add(new Node("epsilon", false, false, false, true, false));
            return init_node;
        }
        public Node create_quest_lock(Node first)
        {
            Node init_node = new Node("none", false, true, false, false, false);
            first.non_terminal = "epsilon";
            init_node.nexts.Add(first);

            Node aux = first;
            while (aux.nexts.Count > 0)
                aux = aux.nexts[0];

            aux.nexts.Add(new Node("epsilon", false, false, false, true, false));
            return init_node;
        }

        //Enumerate all nodes
        public void enumerate_afn(Node pivot, int id)
        {
            pivot.id_node = id++;

            if(pivot.is_Or)
            {
                Node aux = pivot.nexts[1];
                for(int i = 1; i < pivot.nexts.Count; i++)
                {
                    aux = pivot.nexts[i];
                    while (aux.nexts.Count > 0)
                    {
                        aux.id_node = id++;
                        aux = aux.nexts[0];
                        if (aux.is_Or)
                            enumerate_afn(aux, id);
                    }
                    aux.id_node = id++;
                }
                enumerate_afn(pivot.nexts[0], id);
            }
            else
            {
                Node aux = pivot.nexts[0];
                while (aux.nexts.Count > 0)
                {
                    aux.id_node = id++;
                    aux = aux.nexts[0];
                    if (aux.is_Or)
                    {
                        enumerate_afn(aux, id);
                        break;
                    }
                }
                if(aux.nexts.Count == 0)
                {
                    aux.id_node = id;
                    return;
                }
            }

        }

        public int get_accept_number(Node pivot)
        {
            Node aux = pivot.nexts[0];
            while (aux.nexts.Count > 0)
            {
                aux = aux.nexts[0];
            }
            if (aux.nexts.Count == 0)
                return aux.id_node;
            else
                return -1;
        }
    }
}
