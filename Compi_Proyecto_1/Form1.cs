using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compi_Proyecto_1
{
    public partial class Form1 : Form
    {
        List<Set> sets;
        List<Regular_Expression> expressions;

        public Form1()
        {
            InitializeComponent();
            openFile.Filter = "Text|*.er";
            openFile.InitialDirectory = "c:\\";
            openFile.RestoreDirectory = true;
            addTab();
            type_image.Items.Add("AFN");
            sets = new List<Set>();
            expressions = new List<Regular_Expression>();
        }

        private void addTab()
        {
            TabPage tp = new TabPage("Page");
            tabControl.TabPages.Add(tp);

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.Multiline = true;
            tb.WordWrap = false;
            tb.ScrollBars = ScrollBars.Both;
            tp.Controls.Add(tb);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK) // Test result.
            {
                string file = openFile.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    if(tabControl.SelectedTab.Controls[0].Text.Equals(""))
                    {
                        tabControl.SelectedTab.Controls[0].Text = text;
                    }else
                    {
                        addTab();
                        tabControl.Controls[tabControl.TabCount-1].Controls[0].Text = text;
                        tabControl.SelectTab(tabControl.TabCount - 1);
                    }
                }
                catch (IOException) { }
            }
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage actual = tabControl.SelectedTab;
            if(actual != null)
                tabControl.TabPages.Remove(actual);
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addTab();
        }

        private void loadThompsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_afn(tabControl.SelectedTab.Controls[0].Text);
        }

        private void create_afn(string content)
        {
            int size_content = content.Length;
            String lexical_expression = "", pattern;
            int pointer = 0, initial_pointer = 0;
            Boolean expression = false, set = false;

            while (true)
            {
                if (content.ElementAt(pointer) == ' ')
                {
                    pointer += 1;
                    continue;
                }
                else if (content.ElementAt(pointer) == '\n')
                {
                    initial_pointer = ++pointer;
                    continue;
                }
                else if (content.Substring(pointer, 2).Equals("//"))
                {
                    while (true)
                    {
                        if (content.ElementAt(pointer) == '\n')
                            break;
                        pointer += 1;
                    }
                    initial_pointer = pointer;
                }
                else if (content.Substring(pointer, 2).Equals("<!"))
                {
                    while (true)
                    {
                        if (content.Substring(pointer, 2).Equals("!>"))
                            break;
                        pointer += 1;
                    }
                    pointer += 1;
                }
                else if (content.Substring(pointer, 5).Equals("conj:", StringComparison.OrdinalIgnoreCase))
                {
                    pointer += 5;
                    while (content.ElementAt(pointer) == ' ')
                        pointer += 1;
                    initial_pointer = pointer;
                    set = true;
                }
                else if (content.Substring(pointer, 2).Equals("->") && expression == false)
                {
                    expression = true;
                    lexical_expression = "";

                    while (true)
                    {
                        if (pointer == initial_pointer || content.ElementAt(initial_pointer) == ' ')
                            break;

                        lexical_expression += content.ElementAt(initial_pointer);
                        initial_pointer += 1;
                    }
                    pointer += 2;
                    while (content.ElementAt(pointer) == ' ')
                        pointer += 1;
                    initial_pointer = pointer;
                }
                else if (expression && content.ElementAt(pointer) == ';')
                {
                    expression = false;
                    pattern = content.Substring(initial_pointer, pointer -initial_pointer);
                    initial_pointer = pointer + 1;
                    if (set == true)
                        sets.Add(new Set(pattern, lexical_expression));
                    else
                        expressions.Add(new Regular_Expression(pattern, lexical_expression));
                    set = false;

                    while (true)
                    {
                        if (content.ElementAt(pointer) == '\n')
                            break;
                        pointer += 1;
                    }
                    initial_pointer = pointer + 1;
                }

                pointer += 1;
                if (content.Substring(pointer, 2).Equals("%%") || size_content <= pointer)
                    break;
            }
            pointer += 2;
            while (pointer <= size_content && !content.Substring(pointer, 2).Equals("%%"))
                pointer += 1;
            pointer += 2;
            
            foreach (Regular_Expression reg in expressions)
            {
                expression_regular.Items.Add(reg.get_lexical_component());
                reg.generate_afn();
                reg.enumerate_afn();
            }
            foreach(Set set1 in sets)
            {
                set1.analize_pattern();
            }
            
        }

        private void button_graph_Click(object sender, EventArgs e)
        {
            string expression = expression_regular.GetItemText(expression_regular.SelectedItem);
            string ruta = Application.StartupPath + "\\Images\\";

            foreach (Regular_Expression exp in expressions)
            {
                if (exp.get_lexical_component().Equals(expression))
                {
                    string type_im = type_image.GetItemText(type_image.SelectedItem);

                    exp.create_afn_graph();
                    if(type_im.Equals("AFN"))
                    {
                        pictureBox.Image = new Bitmap(ruta + exp.get_lexical_component() + "afn.jpg");
                    }
                }
            }
        }
    }
}
